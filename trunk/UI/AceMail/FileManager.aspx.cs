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
using System.IO;
using DataObjects;
using hMail;

namespace AceMail
{
    public partial class FileManager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserInstance ui = (UserInstance)Session["user"];
                this.dtlFiles.ShowFiles();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["demo"]))
            {
                this.popError.Set("Feature removed for demo");
                return;
            }
            uploadFile(this.FileUpload);
        }

        private void uploadFile(FileUpload ful)
        {
            if (ful.HasFile)
            {
                try
                {
                    UserInstance ui = (UserInstance)Session["user"];
                    string rootPath = ui.RootPath();
                    string file = Path.GetFileName(this.FileUpload.FileName);
                    string fullPath = rootPath + @"data\" + file;
                    FileUpload.SaveAs(fullPath);
                    FileInfo finfo = new FileInfo(fullPath);
                    ui.FileAdd(file, finfo.Extension, fullPath);
                    this.dtlFiles.ShowFiles();
                }
                catch (Exception ex)
                {
                    this.popError.Set(ex.Message);
                }
            }
            else
            {
                this.popError.Set("Invalid File Format");
            }
        }
    }
}
