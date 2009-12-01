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
using System.Web.UI.WebControls.WebParts;
using DataObjectsDAL;
using DataObjects;

namespace AceMail.Controls
{
    public partial class NoticePopUp : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            StringBuilder js = new StringBuilder();
            js.AppendLine("function noticePopShow() {");
            js.AppendLine("$pop = jQuery('#ctl00_Body_popNotice_divNoticePop');");
            js.Append("$pop.dialog({modal:true, position:'center', autoOpen:false, overlay:{opacity:0.5, background:'gray'}});");
            js.AppendLine("$pop.parent().appendTo(jQuery('form:first')); $pop.show(); $pop.dialog('open'); return false;}");
            js.AppendLine("function hideNoticePop() {");
            js.AppendLine("$pop.dialog('close'); return false; }");
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "NoticePopShow", js.ToString(), true);
        }

        public void Set()
        {
            this.lblEvent.Text = "Create New Notice";
            this.txtDate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
            this.txtName.Text = string.Empty;
            this.txtNote.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "noticepop", "pageLoad(); noticePopShow();", true);
        }

        public void Set(string noticeId)
        {
            this.lblEvent.Text = "Update Notice";
            this.ViewState["noticeId"] = noticeId;
            UserInstance ui = (UserInstance)Session["user"];
            Notice notice = ui.NoticeGetByID(new Guid(noticeId));
            this.txtName.Text = notice.Name;
            this.txtNote.Text = notice.Notes;
            this.txtDate.Text = notice.Noticedate.Date.ToString("MM/dd/yyyy");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "noticepop", "pageLoad(); noticePopShow();", true);
        }

        public void btnAdd_Click(object sender, EventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            DateTime date = Convert.ToDateTime(Utility.Sanitize(this.txtDate.Text.Trim()));
            string name = Utility.Sanitize(this.txtName.Text.Trim());
            string notes = Utility.Sanitize(this.txtNote.Text.Trim());
            string noticeId = Convert.ToString(this.ViewState["noticeId"]);
            if (!string.IsNullOrEmpty(noticeId))
                ui.NoticeUpdate(new Guid(noticeId), date, name, notes);
            else
                ui.NoticeAdd(1, date, name, notes);
            Response.Redirect("Home.aspx", false);
        }
    }
}