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
    /// Controller class for userclients
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class UserclientController
    {
        // Preload our schema..
        Userclient thisSchemaLoad = new Userclient();
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
        public UserclientCollection FetchAll()
        {
            UserclientCollection coll = new UserclientCollection();
            Query qry = new Query(Userclient.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public UserclientCollection FetchByID(object Userid)
        {
            UserclientCollection coll = new UserclientCollection().Where("userid", Userid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public UserclientCollection FetchByQuery(Query qry)
        {
            UserclientCollection coll = new UserclientCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Userid)
        {
            return (Userclient.Delete(Userid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Userid)
        {
            return (Userclient.Destroy(Userid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Userid,string Clientid)
        {
            Query qry = new Query(Userclient.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Userid", Userid).AND("Clientid", Clientid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Userid,string Clientid)
	    {
		    Userclient item = new Userclient();
		    
            item.Userid = Userid;
            
            item.Clientid = Clientid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Userid,string Clientid)
	    {
		    Userclient item = new Userclient();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Userid = Userid;
				
			item.Clientid = Clientid;
				
	        item.Save(UserName);
	    }
    }
}
