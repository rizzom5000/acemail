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
    /// Controller class for client
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class ClientController
    {
        // Preload our schema..
        Client thisSchemaLoad = new Client();
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
        public ClientCollection FetchAll()
        {
            ClientCollection coll = new ClientCollection();
            Query qry = new Query(Client.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ClientCollection FetchByID(object Clientid)
        {
            ClientCollection coll = new ClientCollection().Where("clientid", Clientid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public ClientCollection FetchByQuery(Query qry)
        {
            ClientCollection coll = new ClientCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Clientid)
        {
            return (Client.Delete(Clientid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Clientid)
        {
            return (Client.Destroy(Clientid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Clientid,string FirstName,string MiddleName,string LastName,string Address1,string Address2,string City,string StateCode,string PostalCode,string CountryCode,string PhonePrimary,string PhoneSecondary,string PhoneMobile,string Photo,DateTime Createddate,DateTime Updateddate)
	    {
		    Client item = new Client();
		    
            item.Clientid = Clientid;
            
            item.FirstName = FirstName;
            
            item.MiddleName = MiddleName;
            
            item.LastName = LastName;
            
            item.Address1 = Address1;
            
            item.Address2 = Address2;
            
            item.City = City;
            
            item.StateCode = StateCode;
            
            item.PostalCode = PostalCode;
            
            item.CountryCode = CountryCode;
            
            item.PhonePrimary = PhonePrimary;
            
            item.PhoneSecondary = PhoneSecondary;
            
            item.PhoneMobile = PhoneMobile;
            
            item.Photo = Photo;
            
            item.Createddate = Createddate;
            
            item.Updateddate = Updateddate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Clientid,string FirstName,string MiddleName,string LastName,string Address1,string Address2,string City,string StateCode,string PostalCode,string CountryCode,string PhonePrimary,string PhoneSecondary,string PhoneMobile,string Photo,DateTime Createddate,DateTime Updateddate)
	    {
		    Client item = new Client();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Clientid = Clientid;
				
			item.FirstName = FirstName;
				
			item.MiddleName = MiddleName;
				
			item.LastName = LastName;
				
			item.Address1 = Address1;
				
			item.Address2 = Address2;
				
			item.City = City;
				
			item.StateCode = StateCode;
				
			item.PostalCode = PostalCode;
				
			item.CountryCode = CountryCode;
				
			item.PhonePrimary = PhonePrimary;
				
			item.PhoneSecondary = PhoneSecondary;
				
			item.PhoneMobile = PhoneMobile;
				
			item.Photo = Photo;
				
			item.Createddate = Createddate;
				
			item.Updateddate = Updateddate;
				
	        item.Save(UserName);
	    }
    }
}
