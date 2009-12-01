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
    /// Controller class for eventfiles
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EventfileController
    {
        // Preload our schema..
        Eventfile thisSchemaLoad = new Eventfile();
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
        public EventfileCollection FetchAll()
        {
            EventfileCollection coll = new EventfileCollection();
            Query qry = new Query(Eventfile.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventfileCollection FetchByID(object Eventid)
        {
            EventfileCollection coll = new EventfileCollection().Where("eventid", Eventid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EventfileCollection FetchByQuery(Query qry)
        {
            EventfileCollection coll = new EventfileCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Eventid)
        {
            return (Eventfile.Delete(Eventid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Eventid)
        {
            return (Eventfile.Destroy(Eventid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Eventid,string Fileid)
        {
            Query qry = new Query(Eventfile.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Eventid", Eventid).AND("Fileid", Fileid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Eventid,string Fileid)
	    {
		    Eventfile item = new Eventfile();
		    
            item.Eventid = Eventid;
            
            item.Fileid = Fileid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Eventid,string Fileid)
	    {
		    Eventfile item = new Eventfile();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Eventid = Eventid;
				
			item.Fileid = Fileid;
				
	        item.Save(UserName);
	    }
    }
}
