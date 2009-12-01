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
    public partial class AceMail : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            if (ui != null)
            {
                int uCred = ui.UserCredentials();
                if (uCred > 0)
                {
                    this.btnAdmin.Visible = true;
                }
                else
                {
                    this.btnAdmin.Visible = false;
                }
                this.lblName.Text = ui.UserEmail();
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx", false);
        }

        protected void btnMail_Click(object sender, EventArgs e)
        {
            Response.Redirect("MailManager.aspx", false);
        }

        protected void btnContact_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContactManager.aspx", false);
        }

        protected void btnFiles_Click(object sender, EventArgs e)
        {
            Response.Redirect("FileManager.aspx", false);
        }

        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdminManager.aspx", false);
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx", false);
        }
    }
}
