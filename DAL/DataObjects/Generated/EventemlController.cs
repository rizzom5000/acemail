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
    /// Controller class for eventemls
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EventemlController
    {
        // Preload our schema..
        Eventeml thisSchemaLoad = new Eventeml();
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
        public EventemlCollection FetchAll()
        {
            EventemlCollection coll = new EventemlCollection();
            Query qry = new Query(Eventeml.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventemlCollection FetchByID(object Eventid)
        {
            EventemlCollection coll = new EventemlCollection().Where("eventid", Eventid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventemlCollection FetchByQuery(Query qry)
        {
            EventemlCollection coll = new EventemlCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Eventid)
        {
            return (Eventeml.Delete(Eventid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Eventid)
        {
            return (Eventeml.Destroy(Eventid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Eventid,string Emlid)
        {
            Query qry = new Query(Eventeml.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Eventid", Eventid).AND("Emlid", Emlid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Eventid,string Emlid)
	    {
		    Eventeml item = new Eventeml();
		    
            item.Eventid = Eventid;
            
            item.Emlid = Emlid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Eventid,string Emlid)
	    {
		    Eventeml item = new Eventeml();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Eventid = Eventid;
				
			item.Emlid = Emlid;
				
	        item.Save(UserName);
	    }
    }
}
