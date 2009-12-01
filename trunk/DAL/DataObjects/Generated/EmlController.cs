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
    /// Controller class for eml
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class EmlController
    {
        // Preload our schema..
        Eml thisSchemaLoad = new Eml();
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
        public EmlCollection FetchAll()
        {
            EmlCollection coll = new EmlCollection();
            Query qry = new Query(Eml.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public EmlCollection FetchByID(object Emlid)
        {
            EmlCollection coll = new EmlCollection().Where("emlid", Emlid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public EmlCollection FetchByQuery(Query qry)
        {
            EmlCollection coll = new EmlCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Emlid)
        {
            return (Eml.Delete(Emlid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Emlid)
        {
            return (Eml.Destroy(Emlid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Emlid,string Emlpath,string Subject,string Fromaddress,int? Type,bool? Answered,bool? Seen,DateTime Createddate,DateTime Updateddate)
	    {
		    Eml item = new Eml();
		    
            item.Emlid = Emlid;
            
            item.Emlpath = Emlpath;
            
            item.Subject = Subject;
            
            item.Fromaddress = Fromaddress;
            
            item.Type = Type;
            
            item.Answered = Answered;
            
            item.Seen = Seen;
            
            item.Createddate = Createddate;
            
            item.Updateddate = Updateddate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Emlid,string Emlpath,string Subject,string Fromaddress,int? Type,bool? Answered,bool? Seen,DateTime Createddate,DateTime Updateddate)
	    {
		    Eml item = new Eml();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Emlid = Emlid;
				
			item.Emlpath = Emlpath;
				
			item.Subject = Subject;
				
			item.Fromaddress = Fromaddress;
				
			item.Type = Type;
				
			item.Answered = Answered;
				
			item.Seen = Seen;
				
			item.Createddate = Createddate;
				
			item.Updateddate = Updateddate;
				
	        item.Save(UserName);
	    }
    }
}
