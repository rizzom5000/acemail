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
using DataObjects;

namespace AceMail
{
    public partial class AdminManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            if (ui.UserCredentials() < 1)
            {
                Response.Redirect("default.aspx");
            }
            this.gvUsers.Set(ui.UsersGetAll());
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            try
            {
                string login = Utility.Sanitize(this.txtLogin.Text.Trim());
                ui.UserAdd(login, Utility.Sanitize(this.txtPass.Text.Trim()), Convert.ToInt32(this.chkAdmin.Checked));
                this.gvUsers.Set(ui.UsersGetAll());
                this.txtLogin.Text = String.Empty;
                this.txtPass.Text = String.Empty;
                this.chkAdmin.Checked = false;
                this.lblConfirm.Text = "New user " + login + " has successfully been added to the AceMail system.";
            }
            catch (Exception ex)
            {
                this.popError.Set(ex.Message);
            }
        }
    }
}
