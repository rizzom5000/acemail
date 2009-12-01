/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Text;
using DataObjects;
using hMail;

namespace AceMail
{
    public partial class Message : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MailType messageType = (MailType)Enum.Parse(typeof(MailType), Convert.ToString(Request.QueryString[0]), true);
                DateTime messageDate = Convert.ToDateTime(Request.QueryString[2]);
                hMailInstance hmi = (hMailInstance)Session["mail"];
                MailMessage mm = null;
                try
                {
                    if (messageType == MailType.inbox)
                    {
                        int messageID = Convert.ToInt32(Request.QueryString[1]);
                        mm = hmi.MailMessageGetByID(messageID);
                    }
                    else
                    {
                        string messageGuid = Convert.ToString(Request.QueryString[1]);
                        mm = hmi.MailMessageGetByGuid(messageGuid, messageType);
                    }
                    if (mm.From != null)
                        this.lblFromAddress.Text = mm.From.Address;
                    foreach (MailAddress toAddr in mm.To)
                        this.lblToAddress.Text += toAddr.Address + ";";
                    if (mm.CC.Count > 0)
                    {
                        foreach (MailAddress ccAddr in mm.CC)
                            this.lblCCAddress.Text += ccAddr.Address + ";";
                    }
                    this.lblSubjectText.Text = mm.Subject;
                    this.lblDate.Text = messageDate.ToString();
                    if (mm.Attachments != null)
                    {
                        int count = mm.Attachments.Count;
                        if (count > 0)
                        {
                            UserInstance ui = (UserInstance)Session["user"];
                            setAttachments(ui.RootPath() + @"temp\");
                        }
                    }
                    if (!mm.IsBodyHtml)
                        this.messagebody.InnerText = mm.Body;
                    else
                        this.messagebody.InnerHtml = mm.Body;
                }
                catch (Exception ex)
                {
                    this.popError.Set(ex.Message);
                }
                finally
                {
                    if (mm != null)
                        mm.Dispose();
                }
            }
        }

        protected void btnReply_Click(object sender, EventArgs e)
       {
            string messageType = Convert.ToString(Request.QueryString[0]);
            string messageGuid = Convert.ToString(Request.QueryString[1]);
            Response.Redirect("MailCreate.aspx?type=" + messageType + "&id=" + messageGuid + "&rt=reply");
        }

        protected void btnReplyAll_Click(object sender, EventArgs e)
        {
            string messageType = Convert.ToString(Request.QueryString[0]);
            string messageGuid = Convert.ToString(Request.QueryString[1]);
            Response.Redirect("MailCreate.aspx?type=" + messageType + "&id=" + messageGuid + "&rt=replyall");
        }

        protected void dtlAtts_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("view"))
            {
                string path = e.CommandArgument.ToString();
                Response.Redirect("FileView.aspx?type=p&id=" + e.CommandArgument.ToString());
            }
            if (e.CommandName.Equals("download"))
            {
                string path = e.CommandArgument.ToString();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(path));
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.WriteFile(e.CommandArgument.ToString());
                HttpContext.Current.Response.End();
            }
        }

        private void setAttachments(string root)
        {
            Hashtable ht = new Hashtable();
            DirectoryInfo dinfo = new DirectoryInfo(root);
            FileInfo[] files = dinfo.GetFiles();
            if (files.Length > 0)
            {
                foreach (FileInfo file in files)
                {
                    System.Net.Mail.Attachment att;
                    switch (file.Extension)
                    {
                        case ".pdf":
                            att = new System.Net.Mail.Attachment(file.FullName, MediaTypeNames.Application.Pdf);
                            break;
                        case ".rtf":
                            att = new System.Net.Mail.Attachment(file.FullName, MediaTypeNames.Application.Rtf);
                            break;
                        case ".zip":
                            att = new System.Net.Mail.Attachment(file.FullName, MediaTypeNames.Application.Zip);
                            break;
                        default:
                            att = new System.Net.Mail.Attachment(file.FullName, MediaTypeNames.Application.Octet);
                            break;
                    }
                    ht.Add(file.FullName, file.Name);
                }
                this.dtlAtts.DataSource = ht;
                this.dtlAtts.DataBind();
            }
        }
    }
}
