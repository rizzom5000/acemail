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
    /// Controller class for noticefiles
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class NoticefileController
    {
        // Preload our schema..
        Noticefile thisSchemaLoad = new Noticefile();
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
        public NoticefileCollection FetchAll()
        {
            NoticefileCollection coll = new NoticefileCollection();
            Query qry = new Query(Noticefile.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticefileCollection FetchByID(object Noticeid)
        {
            NoticefileCollection coll = new NoticefileCollection().Where("noticeid", Noticeid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticefileCollection FetchByQuery(Query qry)
        {
            NoticefileCollection coll = new NoticefileCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Noticeid)
        {
            return (Noticefile.Delete(Noticeid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Noticeid)
        {
            return (Noticefile.Destroy(Noticeid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Noticeid,string Fileid)
        {
            Query qry = new Query(Noticefile.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Noticeid", Noticeid).AND("Fileid", Fileid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Noticeid,string Fileid)
	    {
		    Noticefile item = new Noticefile();
		    
            item.Noticeid = Noticeid;
            
            item.Fileid = Fileid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Noticeid,string Fileid)
	    {
		    Noticefile item = new Noticefile();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Noticeid = Noticeid;
				
			item.Fileid = Fileid;
				
	        item.Save(UserName);
	    }
    }
}
