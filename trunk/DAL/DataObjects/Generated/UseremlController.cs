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
    /// Controller class for useremls
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class UseremlController
    {
        // Preload our schema..
        Usereml thisSchemaLoad = new Usereml();
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
        public UseremlCollection FetchAll()
        {
            UseremlCollection coll = new UseremlCollection();
            Query qry = new Query(Usereml.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public UseremlCollection FetchByID(object Userid)
        {
            UseremlCollection coll = new UseremlCollection().Where("userid", Userid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public UseremlCollection FetchByQuery(Query qry)
        {
            UseremlCollection coll = new UseremlCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Userid)
        {
            return (Usereml.Delete(Userid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Userid)
        {
            return (Usereml.Destroy(Userid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Userid,string Emlid)
        {
            Query qry = new Query(Usereml.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Userid", Userid).AND("Emlid", Emlid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Userid,string Emlid)
	    {
		    Usereml item = new Usereml();
		    
            item.Userid = Userid;
            
            item.Emlid = Emlid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Userid,string Emlid)
	    {
		    Usereml item = new Usereml();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Userid = Userid;
				
			item.Emlid = Emlid;
				
	        item.Save(UserName);
	    }
    }
}
