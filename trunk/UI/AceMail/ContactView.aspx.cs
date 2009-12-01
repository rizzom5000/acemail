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
using DataObjectsDAL;
using DataObjects;

namespace AceMail
{
    public partial class ContactView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string clientID = Convert.ToString(Request.QueryString[0]);
                if (clientID.Equals("new"))
                {
                    this.lblClient.Text = "Add a new client";
                }
                else
                {
                    UserInstance ui = (UserInstance)Session["user"];
                    Guid clientGuid = new Guid(clientID);
                    Client client = ui.ClientGetByID(clientGuid);
                    this.lblClient.Text = "Details for " + client.FirstName + " " + client.LastName;
                    this.txtFirst.Enabled = false;
                    this.txtMiddle.Enabled = false;
                    this.txtLast.Enabled = false;
                    this.txtFirst.Text = client.FirstName;
                    this.txtMiddle.Text = client.MiddleName;
                    this.txtLast.Text = client.LastName;
                    this.txtAddr1.Text = client.Address1;
                    this.txtAddr2.Text = client.Address2;
                    this.txtCity.Text = client.City;
                    this.txtState.Text = client.StateCode;
                    this.txtPostal.Text = client.PostalCode;
                    this.txtPhone1.Text = client.PhonePrimary;
                    this.txtPhone2.Text = client.PhoneSecondary;
                    this.txtMobile.Text = client.PhoneMobile;
                    string imgPath = ui.RootPath() + @"\temp\";
                    List<Email> emails = ui.ClientGetAllEmailsByID(clientGuid);
                    if (emails.Count > 0)
                        this.txtEmail.Text = emails[0].Emailaddress;
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            string clientID = Convert.ToString(Request.QueryString[0]);
            Guid id;
            string fName = !string.IsNullOrEmpty(this.txtFirst.Text.Trim()) ? Utility.Sanitize(this.txtFirst.Text.Trim()) : null;
            string mName = !string.IsNullOrEmpty(this.txtMiddle.Text.Trim()) ? Utility.Sanitize(this.txtMiddle.Text.Trim()) : null;
            string lName = !string.IsNullOrEmpty(this.txtLast.Text.Trim()) ? Utility.Sanitize(this.txtLast.Text.Trim()) : null;
            string addr1 = !string.IsNullOrEmpty(this.txtAddr1.Text.Trim()) ? Utility.Sanitize(this.txtAddr1.Text.Trim()) : null;
            string addr2 = !string.IsNullOrEmpty(this.txtAddr2.Text.Trim()) ? Utility.Sanitize(this.txtAddr2.Text.Trim()) : null;
            string city = !string.IsNullOrEmpty(this.txtCity.Text.Trim()) ? Utility.Sanitize(this.txtCity.Text.Trim()) : null;
            string state = !string.IsNullOrEmpty(this.txtState.Text.Trim()) ? Utility.Sanitize(this.txtState.Text.Trim()) : null;
            string zip = !string.IsNullOrEmpty(this.txtPostal.Text.Trim()) ? Utility.Sanitize(this.txtPostal.Text.Trim()) : null;
            string ph1 = !string.IsNullOrEmpty(this.txtPhone1.Text.Trim()) ? Utility.Sanitize(this.txtPhone1.Text.Trim()) : null;
            string ph2 = !string.IsNullOrEmpty(this.txtPhone2.Text.Trim()) ? Utility.Sanitize(this.txtPhone2.Text.Trim()) : null;
            string mob = !string.IsNullOrEmpty(this.txtMobile.Text.Trim()) ? Utility.Sanitize(this.txtMobile.Text.Trim()) : null;
            string mail = !string.IsNullOrEmpty(this.txtEmail.Text.Trim()) ? Utility.Sanitize(this.txtEmail.Text.Trim()) : null;
            if (string.IsNullOrEmpty(fName) || string.IsNullOrEmpty(lName))
            {
                this.popError.Set("You must enter a first and last name.");
                return;
            }
            if (clientID.Equals("new"))
            {
                id = ui.ClientAdd(fName, mName, lName, addr1, addr2, city, state, zip, null, ph1, ph2, mob, null);
                if (mail != null)
                {
                    ui.EmailAdd(id, mail);
                }
                Response.Redirect("ContactManager.aspx", false);
            }
            else
            {
                id = new Guid(clientID);
                this.txtFirst.Enabled = false;
                this.txtMiddle.Enabled = false;
                this.txtLast.Enabled = false;
                ui.ClientUpdateAddress(id, addr1, addr2, city, state, zip, null);
                ui.ClientUpdateField(id, "PhonePrimary", ph1);
                ui.ClientUpdateField(id, "PhoneSecondary", ph2);
                ui.ClientUpdateField(id, "PhoneMobile", mob);
                if (mail != null)
                {
                    ui.ClientUpdateEmail(id, mail);
                }
            }
        }
    }
}
