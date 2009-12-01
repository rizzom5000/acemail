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
    /// Controller class for noticeemls
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class NoticeemlController
    {
        // Preload our schema..
        Noticeeml thisSchemaLoad = new Noticeeml();
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
        public NoticeemlCollection FetchAll()
        {
            NoticeemlCollection coll = new NoticeemlCollection();
            Query qry = new Query(Noticeeml.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticeemlCollection FetchByID(object Noticeid)
        {
            NoticeemlCollection coll = new NoticeemlCollection().Where("noticeid", Noticeid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticeemlCollection FetchByQuery(Query qry)
        {
            NoticeemlCollection coll = new NoticeemlCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Noticeid)
        {
            return (Noticeeml.Delete(Noticeid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Noticeid)
        {
            return (Noticeeml.Destroy(Noticeid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Noticeid,string Emlid)
        {
            Query qry = new Query(Noticeeml.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Noticeid", Noticeid).AND("Emlid", Emlid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Noticeid,string Emlid)
	    {
		    Noticeeml item = new Noticeeml();
		    
            item.Noticeid = Noticeid;
            
            item.Emlid = Emlid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Noticeid,string Emlid)
	    {
		    Noticeeml item = new Noticeeml();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Noticeid = Noticeid;
				
			item.Emlid = Emlid;
				
	        item.Save(UserName);
	    }
    }
}
