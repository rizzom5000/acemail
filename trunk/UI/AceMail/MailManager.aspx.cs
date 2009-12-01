/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Net.Mail;
using DataObjects;
using hMail;

namespace AceMail
{
    public partial class MailManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserInstance ui = (UserInstance)Session["user"];
                hMailInstance mail = ui.UserMailInstance();
                this.gvMessages.SetFooter("Inbox");
                Session["user"] = ui;
                Session["mail"] = mail;
                this.ViewState["folder"] = "inbox";
                gvMessages.BindGrid(MailType.inbox);
                this.btnInbox.Enabled = false;
                this.btnInbox.Attributes.Add("style", "font-weight:bold");
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("MailCreate.aspx?type=-1&id=new&rt=send", false);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            hMailInstance mail = (hMailInstance)Session["mail"];
            MailType folder = (MailType)Enum.Parse(typeof(MailType), Convert.ToString(this.ViewState["folder"]), true);
            try
            {
                int[] indices = gvMessages.GetSelectedIndices();
                if (folder == MailType.inbox)
                {
                    foreach (int idx in indices)
                    {
                        int mailID = gvMessages.GetMailID(idx);
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
                        string guid = gvMessages.GetEmlGuid(idx);
                        string path = ui.EmlGetPathByID(new Guid(guid));
                        mail.MessageDelete(path, folder);
                        ui.EmlDelete(new Guid(guid));
                    }
                }
                else
                {
                    foreach (int idx in indices)
                    {
                        string guid = gvMessages.GetEmlGuid(idx);
                        string path = ui.EmlGetPathByID(new Guid(guid));
                        mail.MessageDelete(path, MailType.deleted);
                        ui.EmlUpdate(new Guid(guid), MailType.deleted);
                    }
                }
                this.gvMessages.BindGrid(folder);
            }
            catch (Exception ex)
            {
                this.popError.Set(ex.Message);
            }
        }

        protected void btnInbox_Click(object sender, EventArgs e)
        {
            hMailInstance mail = (hMailInstance)Session["mail"];
            this.gvMessages.SetFooter("Inbox");
            gvMessages.BindGrid(MailType.inbox);
            this.ViewState["folder"] = "inbox";
            setMenu(MailType.inbox);
        }

        protected void btnDrafts_Click(object sender, EventArgs e)
        {
            hMailInstance mail = (hMailInstance)Session["mail"];
            this.gvMessages.SetFooter("Drafts");
            gvMessages.BindGrid(MailType.drafts);
            this.ViewState["folder"] = "drafts";
            setMenu(MailType.drafts);
        }

        protected void btnSent_Click(object sender, EventArgs e)
        {
            hMailInstance mail = (hMailInstance)Session["mail"];
            this.gvMessages.SetFooter("Sent");
            gvMessages.BindGrid(MailType.sent);
            this.ViewState["folder"] = "sent";
            setMenu(MailType.sent);
        }

        protected void btnEvents_Click(object sender, EventArgs e)
        {
            hMailInstance mail = (hMailInstance)Session["mail"];
            this.gvMessages.SetFooter("Events");
            gvMessages.BindGrid(MailType.events);
            this.ViewState["folder"] = "events";
            setMenu(MailType.events);
        }

        protected void btnStorage_Click(object sender, EventArgs e)
        {
            hMailInstance mail = (hMailInstance)Session["mail"];
            this.gvMessages.SetFooter("Storage");
            gvMessages.BindGrid(MailType.storage);
            this.ViewState["folder"] = "storage";
            setMenu(MailType.storage);
        }

        protected void btnDeleted_Click(object sender, EventArgs e)
        {
            hMailInstance mail = (hMailInstance)Session["mail"];
            this.gvMessages.SetFooter("Deleted");
            gvMessages.BindGrid(MailType.deleted);
            this.ViewState["folder"] = "deleted";
            setMenu(MailType.deleted);
        }

        private void setMenu(MailType folder)
        {
            this.btnInbox.Enabled = true;
            this.btnInbox.Attributes.Add("style", "font-weight:normal");
            this.btnDrafts.Enabled = true;
            this.btnDrafts.Attributes.Add("style", "font-weight:normal");
            this.btnSent.Enabled = true;
            this.btnSent.Attributes.Add("style", "font-weight:normal");
            this.btnStorage.Enabled = true;
            this.btnStorage.Attributes.Add("style", "font-weight:normal");
            this.btnEvents.Enabled = true;
            this.btnEvents.Attributes.Add("style", "font-weight:normal");
            this.btnDeleted.Enabled = true;
            this.btnDeleted.Attributes.Add("style", "font-weight:normal");
            switch (folder)
            {
                case MailType.inbox:
                    this.btnInbox.Enabled = false;
                    this.btnInbox.Attributes.Add("style", "font-weight:bold");
                    break;
                case MailType.drafts:
                    this.btnDrafts.Enabled = false;
                    this.btnDrafts.Attributes.Add("style", "font-weight:bold");
                    break;
                case MailType.sent:
                    this.btnSent.Enabled = false;
                    this.btnSent.Attributes.Add("style", "font-weight:bold");
                    break;
                case MailType.storage:
                    this.btnStorage.Enabled = false;
                    this.btnStorage.Attributes.Add("style", "font-weight:bold");
                    break;
                case MailType.events:
                    this.btnEvents.Enabled = false;
                    this.btnEvents.Attributes.Add("style", "font-weight:bold");
                    break;
                case MailType.deleted:
                    this.btnDeleted.Enabled = false;
                    this.btnDeleted.Attributes.Add("style", "font-weight:bold");
                    break;
                default:
                    break;
            }
        }

        protected void btnOptions_Click(object sender, EventArgs e)
        {
            Response.Redirect("MailOptions.aspx", false);
        }

        protected void btnContacts_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContactManager.aspx", false);
        }

        protected void btnJunk_Click(object sender, EventArgs e)
        {
            this.popError.Set("Not Implemented");
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            this.popFolder.Set(Convert.ToString(this.ViewState["folder"]));
        }
    }
}

