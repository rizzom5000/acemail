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
    public partial class UserGridView : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        public void Set(List<User> userList)
        {
            if (userList != null)
            {
                this.gvUsers.DataSource = userList;
                this.gvUsers.DataBind();
            }
            else
            {
                this.gvUsers.EmptyDataText = "There are no users in the system.";
            }
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            if (e.CommandName.Equals("delete"))
            {
                string id = Convert.ToString(e.CommandArgument);
                ui.UserDelete(new Guid(id));
            }
        }

        protected void gvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            UserInstance ui = (UserInstance)Session["user"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                User user = (User)e.Row.DataItem;
                if (user != null)
                {
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    switch (ui.UserGetCredentials(new Guid(user.Userid)))
                    {
                        case -1:
                            lblStatus.Text = "Deleted";
                            break;
                        case 0:
                            lblStatus.Text = "User";
                            break;
                        case 1:
                            lblStatus.Text = "Admin";
                            break;
                        default:
                            lblStatus.Text = "No Credentials Found";
                            break;
                    }
                }
            }
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.gvUsers.PageIndex = e.NewPageIndex;
            this.gvUsers.DataBind();
        }
    }
}