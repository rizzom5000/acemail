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
    /// Controller class for eventclients
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EventclientController
    {
        // Preload our schema..
        Eventclient thisSchemaLoad = new Eventclient();
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
        public EventclientCollection FetchAll()
        {
            EventclientCollection coll = new EventclientCollection();
            Query qry = new Query(Eventclient.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventclientCollection FetchByID(object Eventid)
        {
            EventclientCollection coll = new EventclientCollection().Where("eventid", Eventid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventclientCollection FetchByQuery(Query qry)
        {
            EventclientCollection coll = new EventclientCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Eventid)
        {
            return (Eventclient.Delete(Eventid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Eventid)
        {
            return (Eventclient.Destroy(Eventid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Eventid,string Clientid)
        {
            Query qry = new Query(Eventclient.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Eventid", Eventid).AND("Clientid", Clientid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Eventid,string Clientid)
	    {
		    Eventclient item = new Eventclient();
		    
            item.Eventid = Eventid;
            
            item.Clientid = Clientid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Eventid,string Clientid)
	    {
		    Eventclient item = new Eventclient();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Eventid = Eventid;
				
			item.Clientid = Clientid;
				
	        item.Save(UserName);
	    }
    }
}
