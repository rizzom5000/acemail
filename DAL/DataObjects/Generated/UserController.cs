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
    /// Controller class for user
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class UserController
    {
        // Preload our schema..
        User thisSchemaLoad = new User();
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
        public UserCollection FetchAll()
        {
            UserCollection coll = new UserCollection();
            Query qry = new Query(User.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public UserCollection FetchByID(object Userid)
        {
            UserCollection coll = new UserCollection().Where("userid", Userid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public UserCollection FetchByQuery(Query qry)
        {
            UserCollection coll = new UserCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Userid)
        {
            return (User.Delete(Userid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Userid)
        {
            return (User.Destroy(Userid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Userid,string Login,string Password,string ContactEmail,string Firstname,string Lastname,DateTime Createddate,DateTime Updateddate)
	    {
		    User item = new User();
		    
            item.Userid = Userid;
            
            item.Login = Login;
            
            item.Password = Password;
            
            item.ContactEmail = ContactEmail;
            
            item.Firstname = Firstname;
            
            item.Lastname = Lastname;
            
            item.Createddate = Createddate;
            
            item.Updateddate = Updateddate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Userid,string Login,string Password,string ContactEmail,string Firstname,string Lastname,DateTime Createddate,DateTime Updateddate)
	    {
		    User item = new User();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Userid = Userid;
				
			item.Login = Login;
				
			item.Password = Password;
				
			item.ContactEmail = ContactEmail;
				
			item.Firstname = Firstname;
				
			item.Lastname = Lastname;
				
			item.Createddate = Createddate;
				
			item.Updateddate = Updateddate;
				
	        item.Save(UserName);
	    }
    }
}
