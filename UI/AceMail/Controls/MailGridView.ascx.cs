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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using DataObjectsDAL;
using DataObjects;
using hMail;

namespace AceMail.Controls
{
    public partial class MailGridView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        public void Set(List<hMailMessage> messages)
        {
            this.gvMessages.Visible = true;
            this.lblEmpty.Visible = false;
            if (messages != null)
            {
                this.gvMessages.DataSource = messages;
                this.gvMessages.DataBind();
            }
            else
            {
                this.lblEmpty.Text = "Your Inbox is Empty.";
                this.lblEmpty.Visible = true;
                this.gvMessages.Visible = false;
            }
        }

        public void Set(List<Eml> messages, string folderName)
        {
            this.gvMessages.Visible = true;
            this.lblEmpty.Visible = false;
            if (messages != null)
            {
                this.gvMessages.DataSource = messages;
                this.gvMessages.DataBind();
            }
            else
            {
                this.lblEmpty.Text = "Your " + folderName + " folder is Empty.";
                this.lblEmpty.Visible = true;
                this.gvMessages.Visible = false;
            }
        }

        protected void gvMessages_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            if (e.CommandName.Equals("messages"))
            {
                int idx = Convert.ToInt32(e.CommandArgument);
                object id = gvMessages.DataKeys[idx].Values["emlid"];
                GridViewRow gvr = gvMessages.Rows[idx];
                DateTime date = Convert.ToDateTime(gvr.Cells[2].Text);
                MailType currentFolder = (MailType)Enum.Parse(typeof(MailType), Convert.ToString(this.ViewState["folder"]), true);
                if (currentFolder == MailType.inbox)
                {
                    Response.Redirect("MailView.aspx?type=inbox&id=" + Convert.ToInt32(id) + "&date=" + date, false);
                }
                else
                {
                    string path = ui.EmlGetPathByID(new Guid(id.ToString()));
                    Response.Redirect("MailView.aspx?type=" + currentFolder.ToString() + "&id=" + path + "&date=" + date, false);
                }
            }
        }

        protected void gvMessages_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btn = (LinkButton)e.Row.Cells[1].Controls[0];
                string str = btn.Text;
                btn.ToolTip = str;
                if (e.Row.Cells[0].Text != null && e.Row.Cells[0].Text.Length > 20)
                    e.Row.Cells[0].Text = e.Row.Cells[0].Text.Substring(0, 20) + ".. ";
                if (str != null && str.Length > 20)
                    ((LinkButton)(e.Row.Cells[1].Controls[0])).Text = str.Substring(0, 20) + ".. ";
                if (e.Row.Cells[2].Text != null && e.Row.Cells[2].Text.Length > 20)
                    e.Row.Cells[2].Text = e.Row.Cells[2].Text.Substring(0, 20) + ".. ";
            }
        }

        protected void gvMessages_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            MailType folder = (MailType)Enum.Parse(typeof(MailType), Convert.ToString(this.ViewState["folder"]), true);
            this.BindGrid(folder);
            gvMessages.PageIndex = e.NewPageIndex;
            gvMessages.DataBind();
        }

        protected void gvMessages_Sorting(object sender, GridViewSortEventArgs e)
        {
            switch (e.SortExpression.ToString())
            {
                case "From":
                    break;
                case "Subject":
                    break;
                case "Date":
                    break;
                default:
                    break;
            }
        }

        public void SetFooter(string footer)
        {
            this.gvMessages.Columns[0].FooterText = footer;
        }

        public void BindGrid(MailType folderName)
        {
            this.ViewState["folder"] = folderName.ToString();
            UserInstance ui = (UserInstance)Session["user"];
            hMailInstance mail = (hMailInstance)Session["mail"];
            if (folderName == MailType.inbox)
            {
                List<hMailMessage> msgs = mail.hMailMessagesGetAll(folderName);
                if (msgs == null || msgs.Count == 0)
                    this.Set(null);
                else
                    this.Set(msgs);
            }
            else
            {
                List<Eml> emsgs = ui.EmlGetByType(folderName);
                if (emsgs.Count == 0)
                    this.Set(null, folderName.ToString());
                else
                    this.Set(emsgs, folderName.ToString());
            }
        }

        public int[] GetSelectedIndices()
        {
            List<int> indices = new List<int>();
            foreach (GridViewRow gvr in gvMessages.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("chkSelect");
                if (cb != null && cb.Checked)
                    indices.Add(Convert.ToInt32(gvr.RowIndex));
            }
            return indices.ToArray();
        }

        public int GetMailID(int index)
        {
            object id = this.gvMessages.DataKeys[index].Values["emlid"];
            return Convert.ToInt32(id);
        }

        public string GetEmlGuid(int index)
        {
            object id = gvMessages.DataKeys[index].Values["emlid"];
            return Convert.ToString(id);
        }
    }
}