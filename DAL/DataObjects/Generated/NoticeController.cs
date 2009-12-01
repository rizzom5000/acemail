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
    /// Controller class for notice
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class NoticeController
    {
        // Preload our schema..
        Notice thisSchemaLoad = new Notice();
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
        public NoticeCollection FetchAll()
        {
            NoticeCollection coll = new NoticeCollection();
            Query qry = new Query(Notice.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticeCollection FetchByID(object Noticeid)
        {
            NoticeCollection coll = new NoticeCollection().Where("noticeid", Noticeid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticeCollection FetchByQuery(Query qry)
        {
            NoticeCollection coll = new NoticeCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Noticeid)
        {
            return (Notice.Delete(Noticeid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Noticeid)
        {
            return (Notice.Destroy(Noticeid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Noticeid,string Name,string Userid,int Type,DateTime Noticedate,string Notes,DateTime Createddate,DateTime Updateddate)
	    {
		    Notice item = new Notice();
		    
            item.Noticeid = Noticeid;
            
            item.Name = Name;
            
            item.Userid = Userid;
            
            item.Type = Type;
            
            item.Noticedate = Noticedate;
            
            item.Notes = Notes;
            
            item.Createddate = Createddate;
            
            item.Updateddate = Updateddate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Noticeid,string Name,string Userid,int Type,DateTime Noticedate,string Notes,DateTime Createddate,DateTime Updateddate)
	    {
		    Notice item = new Notice();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Noticeid = Noticeid;
				
			item.Name = Name;
				
			item.Userid = Userid;
				
			item.Type = Type;
				
			item.Noticedate = Noticedate;
				
			item.Notes = Notes;
				
			item.Createddate = Createddate;
				
			item.Updateddate = Updateddate;
				
	        item.Save(UserName);
	    }
    }
}
