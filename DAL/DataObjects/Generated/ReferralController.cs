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
    /// Controller class for referral
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class ReferralController
    {
        // Preload our schema..
        Referral thisSchemaLoad = new Referral();
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
        public ReferralCollection FetchAll()
        {
            ReferralCollection coll = new ReferralCollection();
            Query qry = new Query(Referral.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public ReferralCollection FetchByID(object Referralid)
        {
            ReferralCollection coll = new ReferralCollection().Where("referralid", Referralid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public ReferralCollection FetchByQuery(Query qry)
        {
            ReferralCollection coll = new ReferralCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Referralid)
        {
            return (Referral.Delete(Referralid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Referralid)
        {
            return (Referral.Destroy(Referralid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Referralid,string ReferredClientID,string ReferredByID,DateTime ReferralDate,string Notes,DateTime Createddate,DateTime Updateddate)
	    {
		    Referral item = new Referral();
		    
            item.Referralid = Referralid;
            
            item.ReferredClientID = ReferredClientID;
            
            item.ReferredByID = ReferredByID;
            
            item.ReferralDate = ReferralDate;
            
            item.Notes = Notes;
            
            item.Createddate = Createddate;
            
            item.Updateddate = Updateddate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Referralid,string ReferredClientID,string ReferredByID,DateTime ReferralDate,string Notes,DateTime Createddate,DateTime Updateddate)
	    {
		    Referral item = new Referral();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Referralid = Referralid;
				
			item.ReferredClientID = ReferredClientID;
				
			item.ReferredByID = ReferredByID;
				
			item.ReferralDate = ReferralDate;
				
			item.Notes = Notes;
				
			item.Createddate = Createddate;
				
			item.Updateddate = Updateddate;
				
	        item.Save(UserName);
	    }
    }
}
