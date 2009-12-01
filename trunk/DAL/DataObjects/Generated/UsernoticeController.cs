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
    /// Controller class for usernotices
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class UsernoticeController
    {
        // Preload our schema..
        Usernotice thisSchemaLoad = new Usernotice();
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
        public UsernoticeCollection FetchAll()
        {
            UsernoticeCollection coll = new UsernoticeCollection();
            Query qry = new Query(Usernotice.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public UsernoticeCollection FetchByID(object Userid)
        {
            UsernoticeCollection coll = new UsernoticeCollection().Where("userid", Userid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public UsernoticeCollection FetchByQuery(Query qry)
        {
            UsernoticeCollection coll = new UsernoticeCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Userid)
        {
            return (Usernotice.Delete(Userid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Userid)
        {
            return (Usernotice.Destroy(Userid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Userid,string Noticeid)
        {
            Query qry = new Query(Usernotice.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Userid", Userid).AND("Noticeid", Noticeid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Userid,string Noticeid)
	    {
		    Usernotice item = new Usernotice();
		    
            item.Userid = Userid;
            
            item.Noticeid = Noticeid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Userid,string Noticeid)
	    {
		    Usernotice item = new Usernotice();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Userid = Userid;
				
			item.Noticeid = Noticeid;
				
	        item.Save(UserName);
	    }
    }
}
