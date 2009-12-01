/*
Copyright 2009 Andrew Miller
Distributed under the terms of the GNU General Public License
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DataObjectsDAL;
using DataObjects;

namespace AceMail
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.calendar.SelectedDate == DateTime.MinValue)
                this.calendar.SelectedDate = calendar.TodaysDate;
            displayNotices();
        }

        protected void calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            if (ui != null)
            {
                List<Notice> notices = ui.NoticesGetByMonth(this.calendar.TodaysDate);
                if (!e.Day.IsOtherMonth)
                {
                    foreach (Notice notice in notices)
                    {
                        if (notice.Noticedate.Date == e.Day.Date)
                            e.Cell.BackColor = Color.PaleVioletRed;
                    }
                }
            }
        }

        protected void calendar_SelectionChanged(object sender, EventArgs e)
        {
            displayNotices();
        }

        protected void calendar_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            this.calendar.SelectedDate = this.calendar.VisibleDate.Date;
            displayNotices();
        }

        protected void dlNotices_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            if (e.CommandArgument.Equals("view"))
            {
                string id = Convert.ToString(dlNotices.DataKeys[e.Item.ItemIndex]);
                this.popNotice.Set(id);
            }
        }

        protected void btnNotice_Click(object sender, EventArgs e)
        {
            this.popNotice.Set();
        }

        private void displayNotices()
        {
            UserInstance ui = (UserInstance)Session["user"];
            List<Notice> notices = ui.NoticesGetByDate(this.calendar.SelectedDate);
            if (notices.Count > 0)
            {
                this.lblNotice.Text = "You have " + notices.Count.ToString() + " notices";
                this.dlNotices.DataSource = notices;
                this.dlNotices.DataBind();
            }
            else
                lblNotice.Text = "You have no notices";
        }

        protected void dlNotices_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton hlNotice = (LinkButton)e.Item.FindControl("hlNotice");
                if (hlNotice != null)
                {
                    Notice notice = (Notice)e.Item.DataItem;
                    if (string.IsNullOrEmpty(notice.Name))
                        hlNotice.Text = "Un-named";
                }
            }
        }
    }
}
