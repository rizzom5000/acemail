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
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using DataObjects;

namespace AceMail
{
    public partial class FileView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string type = Convert.ToString(Request.QueryString[0]);
                string file = "";
                UserInstance ui = (UserInstance)Session["user"];
                string dataPath = ui.RootPath() + @"data";
                Session["FCKEditor:UserFilesPath"] = dataPath;
                this.editor.Config["CustomConfigurationsPath"] = "/Scripts/fckconfig.js";
                this.editor.ToolbarSet = "Project";
                switch (type)
                {
                    case "g":
                        string fileID = Convert.ToString(Request.QueryString[1]);
                        Guid fid = new Guid(fileID);
                        try
                        {
                            file = ui.FileGetPathByID(fid);
                        }
                        catch (Exception ex)
                        {
                            this.popError.Set(ex.Message);
                        }
                        this.editor.Value = Utility.Sanitize(file);
                        break;
                    case "p":
                        string filePath = Convert.ToString(Request.QueryString[1]);
                        file = "";
                        StreamReader sr = null;
                        try
                        {
                            sr = System.IO.File.OpenText(filePath);
                            file = sr.ReadToEnd();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            sr.Close();
                        }
                        this.editor.Value = Utility.Sanitize(file);
                        break;
                    default:
                        break;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string type = Convert.ToString(Request.QueryString[0]);
            UserInstance ui = (UserInstance)Session["user"];
            string file = Utility.ClearTags(this.editor.Value);
            file = Utility.Sanitize(file);
            switch (type)
            {
                case "g":
                    string fileID = Convert.ToString(Request.QueryString[1]);
                    Guid fid = new Guid(fileID);
                    ui.FileUpdate(fid, file);
                    Response.Redirect("FileManager.aspx", false);
                    break;
                case "p":
                    string filePath = Convert.ToString(Request.QueryString[1]);
                    StreamWriter sw = null;
                    try
                    {
                        sw = new StreamWriter(filePath);
                        sw.Write(file);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        sw.Close();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
