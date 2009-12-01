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
    /// Controller class for eventgroups
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EventgroupController
    {
        // Preload our schema..
        Eventgroup thisSchemaLoad = new Eventgroup();
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
        public EventgroupCollection FetchAll()
        {
            EventgroupCollection coll = new EventgroupCollection();
            Query qry = new Query(Eventgroup.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventgroupCollection FetchByID(object Eventid)
        {
            EventgroupCollection coll = new EventgroupCollection().Where("eventid", Eventid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventgroupCollection FetchByQuery(Query qry)
        {
            EventgroupCollection coll = new EventgroupCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Eventid)
        {
            return (Eventgroup.Delete(Eventid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Eventid)
        {
            return (Eventgroup.Destroy(Eventid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Eventid,string Groupid)
        {
            Query qry = new Query(Eventgroup.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Eventid", Eventid).AND("Groupid", Groupid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Eventid,string Groupid)
	    {
		    Eventgroup item = new Eventgroup();
		    
            item.Eventid = Eventid;
            
            item.Groupid = Groupid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Eventid,string Groupid)
	    {
		    Eventgroup item = new Eventgroup();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Eventid = Eventid;
				
			item.Groupid = Groupid;
				
	        item.Save(UserName);
	    }
    }
}
