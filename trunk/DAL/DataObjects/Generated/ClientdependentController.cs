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
    /// Controller class for clientdependents
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class ClientdependentController
    {
        // Preload our schema..
        Clientdependent thisSchemaLoad = new Clientdependent();
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
        public ClientdependentCollection FetchAll()
        {
            ClientdependentCollection coll = new ClientdependentCollection();
            Query qry = new Query(Clientdependent.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ClientdependentCollection FetchByID(object Clientid)
        {
            ClientdependentCollection coll = new ClientdependentCollection().Where("clientid", Clientid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public ClientdependentCollection FetchByQuery(Query qry)
        {
            ClientdependentCollection coll = new ClientdependentCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Clientid)
        {
            return (Clientdependent.Delete(Clientid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Clientid)
        {
            return (Clientdependent.Destroy(Clientid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Clientid,string Dependentid)
        {
            Query qry = new Query(Clientdependent.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Clientid", Clientid).AND("Dependentid", Dependentid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Clientid,string Dependentid)
	    {
		    Clientdependent item = new Clientdependent();
		    
            item.Clientid = Clientid;
            
            item.Dependentid = Dependentid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Clientid,string Dependentid)
	    {
		    Clientdependent item = new Clientdependent();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Clientid = Clientid;
				
			item.Dependentid = Dependentid;
				
	        item.Save(UserName);
	    }
    }
}
