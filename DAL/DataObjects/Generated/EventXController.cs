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
    /// Controller class for event
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EventXController
    {
        // Preload our schema..
        EventX thisSchemaLoad = new EventX();
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
        public EventXCollection FetchAll()
        {
            EventXCollection coll = new EventXCollection();
            Query qry = new Query(EventX.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventXCollection FetchByID(object Eventid)
        {
            EventXCollection coll = new EventXCollection().Where("eventid", Eventid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventXCollection FetchByQuery(Query qry)
        {
            EventXCollection coll = new EventXCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Eventid)
        {
            return (EventX.Delete(Eventid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Eventid)
        {
            return (EventX.Destroy(Eventid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Eventid,string Name,string Userid,int Type,DateTime Eventdate,string Notes,DateTime Createddate,DateTime Updateddate)
	    {
		    EventX item = new EventX();
		    
            item.Eventid = Eventid;
            
            item.Name = Name;
            
            item.Userid = Userid;
            
            item.Type = Type;
            
            item.Eventdate = Eventdate;
            
            item.Notes = Notes;
            
            item.Createddate = Createddate;
            
            item.Updateddate = Updateddate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Eventid,string Name,string Userid,int Type,DateTime Eventdate,string Notes,DateTime Createddate,DateTime Updateddate)
	    {
		    EventX item = new EventX();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Eventid = Eventid;
				
			item.Name = Name;
				
			item.Userid = Userid;
				
			item.Type = Type;
				
			item.Eventdate = Eventdate;
				
			item.Notes = Notes;
				
			item.Createddate = Createddate;
				
			item.Updateddate = Updateddate;
				
	        item.Save(UserName);
	    }
    }
}
