/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net.Mime;
using DataObjectsDAL;
using DataObjects;
using hMail;

namespace AceMail
{
    public partial class Mail : System.Web.UI.Page
    {
        private const string htmlBegin = "<html><head><title></title></head><body bgcolor=\"#FFFFFF\" text=\"#000000\" leftmargin=\"0\" topmargin=\"0\" marginwidth=\"0\" marginheight=\"0\">";
        private const string htmlEnd =   "</body></html>";

        public enum RecipientType
        {
            send = 1,
            reply = 2,
            replyall = 3,
            notice = 4, 
            draft = 5

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MailType messageType = (MailType)Enum.Parse(typeof(MailType), Convert.ToString(Request.QueryString[0]), true);
                RecipientType recipientType = (RecipientType)Enum.Parse(typeof(RecipientType), Convert.ToString(Request.QueryString[2]), true);
                UserInstance ui = (UserInstance)Session["user"];
                hMailInstance hmi = (hMailInstance)Session["mail"];
                string dataPath = ui.RootPath() + @"data";
                Session["FCKEditor:UserFilesPath"] = dataPath;
                this.editor.Config["CustomConfigurationsPath"] = "/Scripts/fckconfig.js";
                this.editor.ToolbarSet = "Project";
                if (recipientType == RecipientType.notice)
                {
                    this.btnCancel.Click += new EventHandler(cancelEBtn_Click);
                }
                else
                {
                    this.btnCancel.Click += new EventHandler(cancelMBtn_Click);
                }
                if (!Page.IsPostBack)
                {
                    List<Email> emails = ui.UserGetAllEmails();
                    if (emails.Count > 0)
                    {
                        this.lbxAddr.ClearSelection();
                        this.lbxAddr.Attributes.Add("onDblClick", "setAddress()");
                        this.lbxAddr.DataSource = emails;
                        this.lbxAddr.DataValueField = "Emailaddress";
                        this.lbxAddr.DataBind();
                    }
                    else
                    {
                        this.lblAddr.Text = "Your contacts list is emtpy.";
                    }
                    clearTemp();
                    MailMessage mm = null;
                    int messageID = -1;
                    string emlID = "";
                    try
                    {
                        switch (recipientType)
                        {
                            case RecipientType.send:
                                break;
                            case RecipientType.reply:
                                if (messageType == MailType.inbox)
                                {
                                    messageID = Convert.ToInt32(Request.QueryString[1]);
                                    mm = hmi.MailMessageGetByID(messageID);
                                }
                                else
                                {
                                    string messageGuid = Convert.ToString(Request.QueryString[1]);
                                    mm = hmi.MailMessageGetByGuid(messageGuid, messageType);
                                }
                                this.txtTo.Text = mm.From.Address.ToString();
                                this.txtSubject.Text = "re: " + mm.Subject;
                                this.editor.Value = "<br/><br/>" + mm.Body;
                                break;
                            case RecipientType.replyall:
                                if (messageType == MailType.inbox)
                                {
                                    messageID = Convert.ToInt32(Request.QueryString[1]);
                                    mm = hmi.MailMessageGetByID(messageID);
                                }
                                else
                                {
                                    string messageGuid = Convert.ToString(Request.QueryString[1]);
                                    mm = hmi.MailMessageGetByGuid(messageGuid, messageType);
                                }
                                this.txtTo.Text = mm.From.Address.ToString();
                                if (mm.CC.Count > 0)
                                {
                                    MailAddress[] cc = mm.CC.ToArray();
                                    foreach (MailAddress ma in cc)
                                    {
                                        this.txtTo.Text += ma.ToString();
                                    }
                                }
                                this.txtSubject.Text = "re: " + mm.Subject;
                                this.editor.Value = "<br/><br/>" + mm.Body;
                                break;
                            case RecipientType.draft:
                                emlID = Convert.ToString(Request.QueryString[1]);
                                if (!emlID.Equals("new"))
                                {
                                    loadMessage(emlID, MailType.drafts);
                                }
                                break;
                            case RecipientType.notice:
                                emlID = Convert.ToString(Request.QueryString[1]);
                                this.btnSend.Visible = false;
                                this.btnSend2.Visible = false;
                                this.btnSave.Text = "Save Event";
                                if (!emlID.Equals("new"))
                                {
                                    loadMessage(emlID, MailType.events);
                                }
                                break;
                            default:
                                break;
                        }
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
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTo.Text))
            {
                this.popError.Set("Please enter a recipient");
                return;
            }
            UserInstance ui = (UserInstance)Session["user"];
            hMailInstance hmi = (hMailInstance)Session["mail"];
            RecipientType recipientType = (RecipientType)Enum.Parse(typeof(RecipientType), Convert.ToString(Request.QueryString[2]), true);
            string fileName = "";
            MailMessage mm = null;
            try
            {
                mm = createMessage(ui.UserEmail(), ui.RootPath());
                if (recipientType == RecipientType.reply || recipientType == RecipientType.replyall)
                {
                    fileName = hmi.SendMail(mm, MailType.replied);
                }
                else
                {
                    fileName = hmi.SendMail(mm, MailType.sent);
                }
                ui.EmlAdd(fileName, mm.Subject, mm.From.Address, MailType.sent, false, true);
                Response.Redirect("MailManager.aspx");
            }
            catch (Exception ex)
            {
                this.popError.Set(ex.Message);
            }
            finally
            {
                if(mm != null)
                    mm.Dispose();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTo.Text))
            {
                this.popError.Set("Please enter a recipient");
                return;
            }
            UserInstance ui = (UserInstance)Session["user"];
            hMailInstance hmi = (hMailInstance)Session["mail"];
            RecipientType recipientType = (RecipientType)Enum.Parse(typeof(RecipientType), Convert.ToString(Request.QueryString[2]), true);
            string fileName = "";
            MailMessage mm = null;
            try
            {
                mm = createMessage(ui.UserEmail(), ui.RootPath());
                if (recipientType == RecipientType.notice)
                {
                    string eventid = Convert.ToString(Session["eventid"]);
                    fileName = hmi.MessageSave(mm, MailType.events);
                    Guid emlid = ui.EmlAdd(fileName, mm.Subject, ui.UserEmail(), MailType.events, false, true);
                    Response.Redirect("home.aspx", false);
                }
                else
                {
                    fileName = hmi.MessageSave(mm, MailType.drafts);
                    ui.EmlAdd(fileName, mm.Subject, ui.UserEmail(), MailType.drafts, false, true);
                    Response.Redirect("MailManager.aspx", false);
                }
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["demo"]))
            {
                this.popError.Set("Feature removed for demo");
                return;
            }
            uploadFile(this.FileUpload);
            showAttachments();
        }

        protected void dtlAtts_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandName.Equals("remove"))
            {
                string path = e.CommandArgument.ToString();
                try
                {
                    FileInfo finfo = new FileInfo(path);
                    finfo.Delete();
                }
                catch (Exception ex)
                {
                    this.popError.Set(ex.Message);
                }
                showAttachments();
            }
            if (e.CommandName.Equals("edit"))
            {
                string path = e.CommandArgument.ToString();
                Response.Redirect("FileView.aspx?type=p&id=" + e.CommandArgument.ToString());
            }
        }

        private MailMessage createMessage(string from, string path)
        {
            MailMessage mm = new MailMessage();
            try
            {
                MailAddress fromAddr = new MailAddress(from);
                mm.From = fromAddr;
                string[] to = this.txtTo.Text.Split(';');
                string[] cc = this.txtCC.Text.Split(';');
                string[] bcc = this.txtBCC.Text.Split(';');
                if (!string.IsNullOrEmpty(to[0]))
                {
                    foreach (string toAddr in to)
                    {
                        if (!string.IsNullOrEmpty(toAddr) && !toAddr.Equals(" "))
                        {
                            MailAddress tma = new MailAddress(toAddr);
                            mm.To.Add(tma);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(cc[0]))
                {
                    foreach (string ccAddr in cc)
                    {
                        if (!string.IsNullOrEmpty(ccAddr) && !ccAddr.Equals(" "))
                        {
                            MailAddress cma = new MailAddress(ccAddr);
                            mm.CC.Add(cma);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(bcc[0]))
                {
                    foreach (string bccAddr in bcc)
                    {
                        if (!string.IsNullOrEmpty(bccAddr) && !bccAddr.Equals(" "))
                        {
                            MailAddress bma = new MailAddress(bccAddr);
                            mm.Bcc.Add(bma);
                        }
                    }
                }
                mm.Subject = this.txtSubject.Text.Trim();
                if (this.chkHTML.Checked)
                {
                    mm.IsBodyHtml = true;
                    mm.Body = htmlBegin + Utility.Sanitize(editor.Value) + htmlEnd;
                }
                else
                {
                    mm.IsBodyHtml = false;
                    string body = Utility.ClearTags(editor.Value);
                    mm.Body = Utility.Sanitize(body);
                }
                addAttachments(mm, path);
                return mm;
            }
            catch (Exception ex)
            {
                this.popError.Set(ex.Message);
            }
            return null;
        }

        private void loadMessage(string path, MailType type)
        {
            hMailInstance mail = (hMailInstance)Session["mail"];
            MailMessage mm = null;
            try
            {
                mm = mail.MailMessageGetByGuid(path, type);
                string to = "";
                foreach (MailAddress toAddr in mm.To)
                {
                    to += toAddr.Address + ";";
                }
                this.txtTo.Text = to;
                string cc = "";
                if (mm.CC.Count > 0)
                {
                    foreach (MailAddress ccAddr in mm.CC)
                    {
                        cc += ccAddr.Address + ";";
                    }
                }
                this.txtCC.Text = cc;
                this.txtSubject.Text = mm.Subject;
                if (mm.IsBodyHtml)
                {
                    this.chkHTML.Checked = true;
                }
                this.editor.Value = mm.Body;
                showAttachments();
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

        private void addAttachments(MailMessage mm, string path)
        {
            DirectoryInfo dinfo = new DirectoryInfo(path + @"temp\");
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
                    mm.Attachments.Add(att);
                }
            }
        }

        private void showAttachments()
        {
            UserInstance ui = (UserInstance)Session["user"];
            try
            {
                DirectoryInfo dinfo = new DirectoryInfo(ui.RootPath() + @"temp\");
                FileInfo[] files = dinfo.GetFiles();
                int count = files.Length;
                if (count > 0)
                {
                    files.ToList();
                    this.dtlAtts.DataSource = files;
                    this.dtlAtts.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.popError.Set(ex.Message);
            }
        }

        private void uploadFile(FileUpload ful)
        {
            if (ful.HasFile)
            {
                UserInstance ui = (UserInstance)Session["user"];
                try
                {
                    string rootPath = ui.RootPath();
                    string file = Path.GetFileName(this.FileUpload.FileName);
                    FileUpload.SaveAs(rootPath + @"temp\" + file);
                }
                catch (Exception ex)
                {
                    this.popError.Set(ex.Message);
                }
            }
            else
            {
                this.popError.Set("Invalid File Format");
            }
        }

        private void clearTemp()
        {
            UserInstance ui = (UserInstance)Session["user"];
            DirectoryInfo dinfo = new DirectoryInfo(ui.RootPath() + @"temp\");
            FileInfo[] files = dinfo.GetFiles();
            int fileok = 0;
            int num = files.Length;
            while (num > 0 && fileok < 5)
            {
                foreach (FileInfo finfo in files)
                {
                    try
                    {
                        finfo.Delete();
                        num--;
                    }
                    catch (Exception ex)
                    {
                        /*ugly hack to prevent MailMessage file locking
                         * despite not only using MailMessage.Dispose() and dispose()
                         * on each attachment in every MailMessage object...*/
                        Thread.Sleep(600);
                        fileok++;
                    }
                }
            }
        }

        protected void cancelMBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MailManager.aspx", false);
        }

        protected void cancelEBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx", false);
        }

        public void Set(List<Email> emails, string valueField)
        {
            if (emails != null)
            {
                this.lbxAddr.ClearSelection();
                this.lbxAddr.Attributes.Add("onDblClick", "setAddress()");
                this.lbxAddr.DataSource = emails;
                this.lbxAddr.DataValueField = "Emailaddress";
                this.lbxAddr.DataBind();
            }
            else
            {
                this.lblAddr.Text = "Your contacts list is emtpy.";
            }
        }
    }
}
