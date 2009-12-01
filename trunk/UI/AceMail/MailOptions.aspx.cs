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
using hMail;

namespace AceMail
{
    public partial class MailOptions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hMailInstance hmi = (hMailInstance)Session["mail"];
                this.chkForward.Checked = hmi.ForwardEnabled;
                this.txtForward.Enabled = false;
                if (hmi.ForwardEnabled)
                {
                    this.txtForward.Text = Utility.Sanitize(hmi.ForwardAddress);
                    this.txtForward.Enabled = true;
                }
                this.chkVacation.Checked = hmi.VacationMessageEnabled;
                this.txtVacation.Enabled = false;
                if (hmi.VacationMessageEnabled)
                {
                    this.txtVacation.Text = Utility.Sanitize(hmi.VacationMessageText);
                    this.txtVacation.Enabled = true;
                }
            }
        }

        protected void chkForward_CheckedChanged(object sender, EventArgs e)
        {
            this.txtForward.Enabled = !this.txtForward.Enabled;
        }

        protected void chkVacation_CheckedChanged(object sender, EventArgs e)
        {
            this.txtVacation.Enabled = !this.txtVacation.Enabled;
        }

        protected void btnOptions_Click(object sender, EventArgs e)
        {
            hMailInstance hmi = (hMailInstance)Session["mail"];
            if (this.chkForward.Checked && !string.IsNullOrEmpty(this.txtForward.Text.Trim()))
            {
                hmi.ForwardEnabled = true;
                hmi.ForwardAddress = Utility.Sanitize(this.txtForward.Text.Trim());
            }
            else
            {
                hmi.ForwardEnabled = false;
            }
            if (this.chkVacation.Checked && !string.IsNullOrEmpty(this.txtVacation.Text.Trim()))
            {
                hmi.VacationMessageEnabled = true;
                hmi.VacationMessageText = Utility.Sanitize(this.txtVacation.Text.Trim());
            }
            else
            {
                hmi.VacationMessageEnabled = false;
            }
            Response.Redirect("MailManager.aspx");
        }
    }
}
