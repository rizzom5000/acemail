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
    /// Controller class for noticeclients
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class NoticeclientController
    {
        // Preload our schema..
        Noticeclient thisSchemaLoad = new Noticeclient();
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
        public NoticeclientCollection FetchAll()
        {
            NoticeclientCollection coll = new NoticeclientCollection();
            Query qry = new Query(Noticeclient.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticeclientCollection FetchByID(object Noticeid)
        {
            NoticeclientCollection coll = new NoticeclientCollection().Where("noticeid", Noticeid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public NoticeclientCollection FetchByQuery(Query qry)
        {
            NoticeclientCollection coll = new NoticeclientCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Noticeid)
        {
            return (Noticeclient.Delete(Noticeid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Noticeid)
        {
            return (Noticeclient.Destroy(Noticeid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Noticeid,string Clientid)
        {
            Query qry = new Query(Noticeclient.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Noticeid", Noticeid).AND("Clientid", Clientid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Noticeid,string Clientid)
	    {
		    Noticeclient item = new Noticeclient();
		    
            item.Noticeid = Noticeid;
            
            item.Clientid = Clientid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Noticeid,string Clientid)
	    {
		    Noticeclient item = new Noticeclient();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Noticeid = Noticeid;
				
			item.Clientid = Clientid;
				
	        item.Save(UserName);
	    }
    }
}
