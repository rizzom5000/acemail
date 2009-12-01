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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DataObjects;

namespace AceMail
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txtLogin.Text) && !string.IsNullOrEmpty(txtPass.Text))
                {
                    UserInstance ui = new UserInstance(Utility.Sanitize(txtLogin.Text.Trim()), Utility.Sanitize(txtPass.Text.Trim()));
                    Session["user"] = ui;
                    Response.Redirect("Home.aspx", false);
                }
                else
                    this.popError.Set("Please Enter Login Credentials");
            }
            catch (Exception ex)
            {
                this.popError.Set("Login Failed <br/>" + ex.Message);
            }
        }
    }
}
