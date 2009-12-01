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
    /// Controller class for usergroups
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class UsergroupController
    {
        // Preload our schema..
        Usergroup thisSchemaLoad = new Usergroup();
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
        public UsergroupCollection FetchAll()
        {
            UsergroupCollection coll = new UsergroupCollection();
            Query qry = new Query(Usergroup.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public UsergroupCollection FetchByID(object Userid)
        {
            UsergroupCollection coll = new UsergroupCollection().Where("userid", Userid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public UsergroupCollection FetchByQuery(Query qry)
        {
            UsergroupCollection coll = new UsergroupCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Userid)
        {
            return (Usergroup.Delete(Userid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Userid)
        {
            return (Usergroup.Destroy(Userid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Userid,string Groupid)
        {
            Query qry = new Query(Usergroup.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Userid", Userid).AND("Groupid", Groupid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Userid,string Groupid)
	    {
		    Usergroup item = new Usergroup();
		    
            item.Userid = Userid;
            
            item.Groupid = Groupid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Userid,string Groupid)
	    {
		    Usergroup item = new Usergroup();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Userid = Userid;
				
			item.Groupid = Groupid;
				
	        item.Save(UserName);
	    }
    }
}
