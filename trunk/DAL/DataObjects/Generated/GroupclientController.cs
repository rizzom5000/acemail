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
    /// Controller class for groupclients
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class GroupclientController
    {
        // Preload our schema..
        Groupclient thisSchemaLoad = new Groupclient();
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
        public GroupclientCollection FetchAll()
        {
            GroupclientCollection coll = new GroupclientCollection();
            Query qry = new Query(Groupclient.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public GroupclientCollection FetchByID(object Groupid)
        {
            GroupclientCollection coll = new GroupclientCollection().Where("groupid", Groupid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public GroupclientCollection FetchByQuery(Query qry)
        {
            GroupclientCollection coll = new GroupclientCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Groupid)
        {
            return (Groupclient.Delete(Groupid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Groupid)
        {
            return (Groupclient.Destroy(Groupid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Groupid,string Clientid)
        {
            Query qry = new Query(Groupclient.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Groupid", Groupid).AND("Clientid", Clientid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Groupid,string Clientid,string Clientemailid,string Clientsmsid)
	    {
		    Groupclient item = new Groupclient();
		    
            item.Groupid = Groupid;
            
            item.Clientid = Clientid;
            
            item.Clientemailid = Clientemailid;
            
            item.Clientsmsid = Clientsmsid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Groupid,string Clientid,string Clientemailid,string Clientsmsid)
	    {
		    Groupclient item = new Groupclient();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Groupid = Groupid;
				
			item.Clientid = Clientid;
				
			item.Clientemailid = Clientemailid;
				
			item.Clientsmsid = Clientsmsid;
				
	        item.Save(UserName);
	    }
    }
}
