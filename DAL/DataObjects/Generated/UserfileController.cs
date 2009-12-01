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
    /// Controller class for userfiles
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class UserfileController
    {
        // Preload our schema..
        Userfile thisSchemaLoad = new Userfile();
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
        public UserfileCollection FetchAll()
        {
            UserfileCollection coll = new UserfileCollection();
            Query qry = new Query(Userfile.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public UserfileCollection FetchByID(object Userid)
        {
            UserfileCollection coll = new UserfileCollection().Where("userid", Userid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public UserfileCollection FetchByQuery(Query qry)
        {
            UserfileCollection coll = new UserfileCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Userid)
        {
            return (Userfile.Delete(Userid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Userid)
        {
            return (Userfile.Destroy(Userid) == 1);
        }
        
        
        
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(string Userid,string Fileid)
        {
            Query qry = new Query(Userfile.Schema);
            qry.QueryType = QueryType.Delete;
            qry.AddWhere("Userid", Userid).AND("Fileid", Fileid);
            qry.Execute();
            return (true);
        }        
       
    	
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Userid,string Fileid)
	    {
		    Userfile item = new Userfile();
		    
            item.Userid = Userid;
            
            item.Fileid = Fileid;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Userid,string Fileid)
	    {
		    Userfile item = new Userfile();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Userid = Userid;
				
			item.Fileid = Fileid;
				
	        item.Save(UserName);
	    }
    }
}
