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
    public partial class FileDataList : System.Web.UI.UserControl
    {
        private const int PAGES = 10;

        protected void Page_Load(object sender, EventArgs e)
        {}

        public void ShowFiles()
        {
            int curr;
            if (this.ViewState["page"] == null)
            {
                curr = 1;
                this.ViewState["page"] = 1;
            }
            else
                curr = Convert.ToInt32(this.ViewState["page"]);
            UserInstance ui = (UserInstance)Session["user"];
            List<File> userFiles = ui.FilesGetAll();
            if (userFiles.Count > PAGES)
            {
                PagedDataSource pgdFiles = new PagedDataSource();
                pgdFiles.DataSource = userFiles;
                pgdFiles.AllowPaging = true;
                pgdFiles.PageSize = PAGES;
                pgdFiles.CurrentPageIndex = curr - 1;
                this.btnPrev.Visible = true;
                this.btnNext.Visible = true;
                this.btnPrev.Enabled = !pgdFiles.IsFirstPage;
                this.btnNext.Enabled = !pgdFiles.IsLastPage;
                this.lblPage.Text = "Page: " + (curr).ToString() + " of " + pgdFiles.PageCount.ToString();
                this.dtlFiles.DataSource = pgdFiles;
                this.dtlFiles.DataBind();
            }
            else if (userFiles.Count == 0)
            {
                this.dtlFiles.Visible = false;
                this.btnPrev.Visible = false;
                this.btnNext.Visible = false;
                this.lblPage.Text = "You have no files to view.";
            }
            else
            {
                this.lblPage.Text = "Page: " + (curr).ToString() + " of 1";
                this.btnPrev.Visible = false;
                this.btnNext.Visible = false;
                this.dtlFiles.Visible = true;
                this.dtlFiles.DataSource = userFiles;
                this.dtlFiles.DataBind();
            }
        }

        protected void dtlFiles_ItemCommand(object sender, DataListCommandEventArgs e)
        {
            string fileid = e.CommandArgument.ToString();
            UserInstance ui = (UserInstance)Session["user"];
            if (e.CommandName.Equals("delete"))
            {
                ui.FileRemove(new Guid(fileid));
                this.ShowFiles();
            }
            if (e.CommandName.Equals("edit"))
            {
                Response.Redirect("FileView.aspx?type=g&id=" + fileid);
            }
            if (e.CommandName.Equals("download"))
            {
                File file = ui.FileGetByID(new Guid(fileid));
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Address);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.WriteFile(file.Address);
                HttpContext.Current.Response.End();
            }
        }


        protected void btnPrev_Click(object sender, EventArgs e)
        {
            int curr = Convert.ToInt32(this.ViewState["page"]);
            curr--;
            this.ViewState["page"] = curr;
            this.ShowFiles();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            int curr = Convert.ToInt32(this.ViewState["page"]);
            curr++;
            this.ViewState["page"] = curr;
            this.ShowFiles();
        }
    }
}