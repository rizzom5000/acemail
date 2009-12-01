/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net.Mail;
using DataObjects;
using hMail;

namespace AceMail.Controls
{
    public partial class FolderPopUp : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            StringBuilder js = new StringBuilder();
            js.AppendLine("function folderPopShow() {");
            js.AppendLine("$pop = jQuery('#ctl00_Body_popFolder_divFolderPop');");
            js.Append("$pop.dialog({modal:true, position:'center', autoOpen:false, overlay:{opacity:0.5, background:'gray'}});");
            js.AppendLine("$pop.parent().appendTo(jQuery('form:first')); $pop.show(); $pop.dialog('open'); return false;}");
            js.AppendLine("function hideFolderPop() {");
            js.AppendLine("$pop.dialog('close'); return false; }");
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "FolderPopShow", js.ToString(), true);
        }

        public void Set(string folder)
        {
            this.ViewState["folder"] = folder;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "folderpop", "folderPopShow();", true);
        }

        protected void btnStorage_Click(object sender, EventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            hMailInstance mail = (hMailInstance)Session["mail"];
            MailType folder = (MailType)Enum.Parse(typeof(MailType), Convert.ToString(this.ViewState["folder"]), true);
            MailGridView messages = (MailGridView)this.Parent.FindControl("gvMessages");
            int[] indices = messages.GetSelectedIndices();
            if (folder == MailType.inbox)
            {
                foreach (int idx in indices)
                {
                    int mailID = messages.GetMailID(idx);
                    string fileName = mail.MessageMove(mailID, MailType.storage);
                    MailMessage mm = mail.MailMessageGetByGuid(fileName, MailType.storage);
                    ui.EmlAdd(fileName, mm.Subject, mm.From.Address, MailType.storage, false, true);
                    if(mm != null)
                        mm.Dispose();
                }
            }
            else
            {
                foreach (int idx in indices)
                {
                    string guid = messages.GetEmlGuid(idx); ;
                    Guid emlguid = new Guid(guid);
                    string path = ui.EmlGetPathByID(emlguid);
                    mail.MessageMove(path, folder, MailType.storage);
                    ui.EmlUpdate(emlguid, MailType.storage);
                }
            }
            messages.BindGrid(folder);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            hMailInstance mail = (hMailInstance)Session["mail"];
            MailType folder = (MailType)Enum.Parse(typeof(MailType), Convert.ToString(this.ViewState["folder"]), true);
            MailGridView messages = (MailGridView)this.Parent.FindControl("gvMessages");
            int[] indices = messages.GetSelectedIndices();
            if (folder == MailType.inbox)
            {
                foreach (int idx in indices)
                {
                    int mailID = messages.GetMailID(idx);
                    string fileName = mail.MessageMove(mailID, MailType.deleted);
                    MailMessage mm = mail.MailMessageGetByGuid(fileName, MailType.deleted);
                    ui.EmlAdd(fileName, mm.Subject, mm.From.Address, MailType.deleted, false, true);
                    if (mm != null)
                        mm.Dispose();
                }
            }
            else if (folder == MailType.deleted)
            {
                foreach (int idx in indices)
                {
                    string guid = messages.GetEmlGuid(idx);
                    string path = ui.EmlGetPathByID(new Guid(guid));
                    mail.MessageDelete(path, folder);
                    ui.EmlDelete(new Guid(guid));
                }
            }
            else
            {
                foreach (int idx in indices)
                {
                    string guid = messages.GetEmlGuid(idx);
                    string path = ui.EmlGetPathByID(new Guid(guid));
                    mail.MessageDelete(path, MailType.deleted);
                    ui.EmlUpdate(new Guid(guid), MailType.deleted);
                }
            }
            messages.BindGrid(folder);
        }

        protected void btnJunk_Click(object sender, EventArgs e)
        {
            throw new Exception("Not Implemented");
        }
    }
}