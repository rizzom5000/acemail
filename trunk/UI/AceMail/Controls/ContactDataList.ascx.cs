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

namespace AceMail.Controls
{
    public partial class ContactDataList : System.Web.UI.UserControl
    {
        private const int PAGES = 10;

        protected void Page_Load(object sender, EventArgs e)
        { }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int curr = Convert.ToInt32(this.ViewState["page"]);
            curr--;
            this.ViewState["page"] = curr;
            this.ShowClients();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int curr = Convert.ToInt32(this.ViewState["page"]);
            curr++;
            this.ViewState["page"] = curr;
            this.ShowClients();
        }

        public void ShowClients()
        {
            int curr;
            if (this.ViewState["page"] == null)
            {
                curr = 1;
                this.ViewState["page"] = 1;
            }
            else
                curr = Convert.ToInt32(this.ViewState["page"]);
            UserInstance ui = (UserInstance)Session["user"];
            List<Client> userClients = ui.ClientsGetAll();
            if (userClients.Count > PAGES)
            {
                PagedDataSource pgdFiles = new PagedDataSource();
                pgdFiles.DataSource = userClients;
                pgdFiles.AllowPaging = true;
                pgdFiles.PageSize = PAGES;
                pgdFiles.CurrentPageIndex = curr - 1;
                this.btnPrev.Visible = true;
                this.btnNext.Visible = true;
                this.btnPrev.Enabled = !pgdFiles.IsFirstPage;
                this.btnNext.Enabled = !pgdFiles.IsLastPage;
                this.lblPage.Text = "Page: " + (curr).ToString() + " of " + pgdFiles.PageCount.ToString();
                this.dtlClients.DataSource = pgdFiles;
                this.dtlClients.DataBind();
            }
            else if (userClients.Count == 0)
            {
                this.dtlClients.Visible = false;
                this.btnPrev.Visible = false;
                this.btnNext.Visible = false;
                this.lblPage.Text = "You have 0 contacts to view.";
            }
            else
            {
                this.lblPage.Text = "Page: " + (curr).ToString() + " of 1";
                this.btnPrev.Visible = false;
                this.btnNext.Visible = false;
                this.dtlClients.Visible = true;
                this.dtlClients.DataSource = userClients;
                this.dtlClients.DataBind();
            }
        }

        protected void dtlClients_ItemDataBound(Object Sender, DataListItemEventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Client c = (Client)e.Item.DataItem;
                LinkButton btnName = ((LinkButton)e.Item.FindControl("btnName"));
                btnName.Text = c.LastName + ", " + c.FirstName;
                btnName.PostBackUrl = @"~\ContactView.aspx?id=" + c.Clientid;
                //support one email for now
                Label lblEmail = ((Label)e.Item.FindControl("lblEmail"));
                List<Email> clientEmails = ui.ClientGetAllEmailsByID(new Guid(c.Clientid));
                if (clientEmails.Count > 0)
                    lblEmail.Text = clientEmails[0].Emailaddress;
                else
                    lblEmail.Text = "no email";
            }
        }

        protected void dtlClients_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            string clientid = e.CommandArgument.ToString();
            UserInstance ui = (UserInstance)Session["user"];
            if (e.CommandName.Equals("btnDelete_Click"))
            {
                ui.ClientRemove(new Guid(clientid));
                this.ShowClients();
            }
        }
    }
}