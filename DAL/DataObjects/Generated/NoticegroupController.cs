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
    /// Controller class for noticegroups
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class NoticegroupController
    {
        // Preload our schema..
        Noticegroup thisSchemaLoad = new Noticegroup();
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
        public NoticegroupCollection FetchAll()
        {
            NoticegroupCollection coll = new NoticegroupCollection();
            Query qry = new Query(Noticegroup.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticegroupCollection FetchByID(object Noticeid)
        {
            NoticegroupCollection coll = new NoticegroupCollection().Where("noticeid", Noticeid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticegroupCollection FetchByQuery(Query qry)
        {
            NoticegroupCollection coll = new NoticegroupCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Noticeid)
        {
            return (Noticegroup.Delete(Noticeid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Noticeid)
        {
            return (Noticegroup.Destroy(Noticeid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Noticeid,string Groupid)
        {
            Query qry = new Query(Noticegroup.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Noticeid", Noticeid).AND("Groupid", Groupid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Noticeid,string Groupid)
	    {
		    Noticegroup item = new Noticegroup();
		    
            item.Noticeid = Noticeid;
            
            item.Groupid = Groupid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Noticeid,string Groupid)
	    {
		    Noticegroup item = new Noticegroup();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Noticeid = Noticeid;
				
			item.Groupid = Groupid;
				
	        item.Save(UserName);
	    }
    }
}
