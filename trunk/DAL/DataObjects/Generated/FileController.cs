using System; 
using System.Text; 
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; 
using System.Xml; 
using System.Xml.Serialization;
using SubSonic; 
using SubSonic.Utilities;
namespace DataObjectsDAL
{
    /// <summary>
    /// Controller class for file
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class FileController
    {
        // Preload our schema..
        File thisSchemaLoad = new File();
        private string userName = String.Empty;
        protected string UserName
        {
            get
            {
				if (userName.Length == 0) 
				{
    				if (System.Web.HttpContext.Current != null)
    				{
						userName=System.Web.HttpContext.Current.User.Identity.Name;
					}
					else
					{
						userName=System.Threading.Thread.CurrentPrincipal.Identity.Name;
					}
				}
				return userName;
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public FileCollection FetchAll()
        {
            FileCollection coll = new FileCollection();
            Query qry = new Query(File.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public FileCollection FetchByID(object Fileid)
        {
            FileCollection coll = new FileCollection().Where("fileid", Fileid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public FileCollection FetchByQuery(Query qry)
        {
            FileCollection coll = new FileCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Fileid)
        {
            return (File.Delete(Fileid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Fileid)
        {
            return (File.Destroy(Fileid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Fileid,string Filename,string Extension,string Address,DateTime Createddate,DateTime Updateddate)
	    {
		    File item = new File();
		    
            item.Fileid = Fileid;
            
            item.Filename = Filename;
            
            item.Extension = Extension;
            
            item.Address = Address;
            
            item.Createddate = Createddate;
            
            item.Updateddate = Updateddate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Fileid,string Filename,string Extension,string Address,DateTime Createddate,DateTime Updateddate)
	    {
		    File item = new File();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Fileid = Fileid;
				
			item.Filename = Filename;
				
			item.Extension = Extension;
				
			item.Address = Address;
				
			item.Createddate = Createddate;
				
			item.Updateddate = Updateddate;
				
	        item.Save(UserName);
	    }
    }
}
