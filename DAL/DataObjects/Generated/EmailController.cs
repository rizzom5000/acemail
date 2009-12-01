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
    /// Controller class for email
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EmailController
    {
        // Preload our schema..
        Email thisSchemaLoad = new Email();
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
        public EmailCollection FetchAll()
        {
            EmailCollection coll = new EmailCollection();
            Query qry = new Query(Email.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EmailCollection FetchByID(object Emailid)
        {
            EmailCollection coll = new EmailCollection().Where("emailid", Emailid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EmailCollection FetchByQuery(Query qry)
        {
            EmailCollection coll = new EmailCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Emailid)
        {
            return (Email.Delete(Emailid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Emailid)
        {
            return (Email.Destroy(Emailid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Emailid,string Ownerid,string Emailaddress,DateTime Createddate,DateTime Updateddate)
	    {
		    Email item = new Email();
		    
            item.Emailid = Emailid;
            
            item.Ownerid = Ownerid;
            
            item.Emailaddress = Emailaddress;
            
            item.Createddate = Createddate;
            
            item.Updateddate = Updateddate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Emailid,string Ownerid,string Emailaddress,DateTime Createddate,DateTime Updateddate)
	    {
		    Email item = new Email();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Emailid = Emailid;
				
			item.Ownerid = Ownerid;
				
			item.Emailaddress = Emailaddress;
				
			item.Createddate = Createddate;
				
			item.Updateddate = Updateddate;
				
	        item.Save(UserName);
	    }
    }
}
