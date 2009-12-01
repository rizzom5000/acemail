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
using DataObjects;

namespace AceMail
{
    public partial class ContactManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserInstance ui = (UserInstance)Session["user"];
                this.ddlContacts.ShowClients();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContactView.aspx?id=new"); 
        }
    }
}
