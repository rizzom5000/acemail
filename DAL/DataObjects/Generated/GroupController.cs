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
    /// Controller class for group
    /// </summary>
    [System.ComponentModel.DataObject]
    public partial class GroupController
    {
        // Preload our schema..
        Group thisSchemaLoad = new Group();
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
        public GroupCollection FetchAll()
        {
            GroupCollection coll = new GroupCollection();
            Query qry = new Query(Group.Schema);
            coll.LoadAndCloseReader(qry.ExecuteReader());
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public GroupCollection FetchByID(object Groupid)
        {
            GroupCollection coll = new GroupCollection().Where("groupid", Groupid).Load();
            return coll;
        }
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
        public GroupCollection FetchByQuery(Query qry)
        {
            GroupCollection coll = new GroupCollection();
            coll.LoadAndCloseReader(qry.ExecuteReader()); 
            return coll;
        }
        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(object Groupid)
        {
            return (Group.Delete(Groupid) == 1);
        }
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public bool Destroy(object Groupid)
        {
            return (Group.Destroy(Groupid) == 1);
        }
        
        
    	
	    /// <summary>
	    /// Inserts a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, true)]
	    public void Insert(string Groupid,string GroupName,string Notes,DateTime Createddate,DateTime Updateddate)
	    {
		    Group item = new Group();
		    
            item.Groupid = Groupid;
            
            item.GroupName = GroupName;
            
            item.Notes = Notes;
            
            item.Createddate = Createddate;
            
            item.Updateddate = Updateddate;
            
	    
		    item.Save(UserName);
	    }
    	
	    /// <summary>
	    /// Updates a record, can be used with the Object Data Source
	    /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, true)]
	    public void Update(string Groupid,string GroupName,string Notes,DateTime Createddate,DateTime Updateddate)
	    {
		    Group item = new Group();
	        item.MarkOld();
	        item.IsLoaded = true;
		    
			item.Groupid = Groupid;
				
			item.GroupName = GroupName;
				
			item.Notes = Notes;
				
			item.Createddate = Createddate;
				
			item.Updateddate = Updateddate;
				
	        item.Save(UserName);
	    }
    }
}
