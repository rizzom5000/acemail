/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AceMail.Controls
{
    public partial class ErrorPopUp : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            StringBuilder js = new StringBuilder();
            js.AppendLine("function errorPopShow() {");
            js.AppendLine("$pop = jQuery('#divErrorPop');");
            js.Append("$pop.dialog({modal:true, position:'center', autoOpen:false, overlay:{opacity:0.5, background:'gray'}});");
            js.AppendLine("$pop.parent().appendTo(jQuery('form:first')); $pop.show(); $pop.dialog('open'); return false;}");
            js.AppendLine("function hideErrorPop() {");
            js.AppendLine("$pop.dialog('close'); return false; }");
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ErrorPopShow", js.ToString(), true);
        }

        public void Set(string message)
        {
            this.lblError.Text = message;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "errorpop", "errorPopShow();", true);
        }
    }
}