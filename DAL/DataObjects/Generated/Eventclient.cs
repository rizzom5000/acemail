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
	/// Strongly-typed collection for the Eventclient class.
	/// </summary>
    [Serializable]
	public partial class EventclientCollection : ActiveList<Eventclient, EventclientCollection>
	{	   
		public EventclientCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>EventclientCollection</returns>
		public EventclientCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Eventclient o = this[i];
                foreach (SubSonic.Where w in this.wheres)
                {
                    bool remove = false;
                    System.Reflection.PropertyInfo pi = o.GetType().GetProperty(w.ColumnName);
                    if (pi.CanRead)
                    {
                        object val = pi.GetValue(o, null);
                        switch (w.Comparison)
                        {
                            case SubSonic.Comparison.Equals:
                                if (!val.Equals(w.ParameterValue))
                                {
                                    remove = true;
                                }
                                break;
                        }
                    }
                    if (remove)
                    {
                        this.Remove(o);
                        break;
                    }
                }
            }
            return this;
        }
		
		
	}
	/// <summary>
	/// This is an ActiveRecord class which wraps the eventclients table.
	/// </summary>
	[Serializable]
	public partial class Eventclient : ActiveRecord<Eventclient>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Eventclient()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Eventclient(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Eventclient(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Eventclient(string columnName, object columnValue)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByParam(columnName,columnValue);
		}
		
		protected static void SetSQLProps() { GetTableSchema(); }
		
		#endregion
		
		#region Schema and Query Accessor	
		public static Query CreateQuery() { return new Query(Schema); }
		public static TableSchema.Table Schema
		{
			get
			{
				if (BaseSchema == null)
					SetSQLProps();
				return BaseSchema;
			}
		}
		
		private static void GetTableSchema() 
		{
			if(!IsSchemaInitialized)
			{
				//Schema declaration
				TableSchema.Table schema = new TableSchema.Table("eventclients", TableType.Table, DataService.GetInstance("AceMail"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"";
				//columns
				
				TableSchema.TableColumn colvarEventid = new TableSchema.TableColumn(schema);
				colvarEventid.ColumnName = "eventid";
				colvarEventid.DataType = DbType.String;
				colvarEventid.MaxLength = 64;
				colvarEventid.AutoIncrement = false;
				colvarEventid.IsNullable = false;
				colvarEventid.IsPrimaryKey = true;
				colvarEventid.IsForeignKey = false;
				colvarEventid.IsReadOnly = false;
				colvarEventid.DefaultSetting = @"";
				colvarEventid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEventid);
				
				TableSchema.TableColumn colvarClientid = new TableSchema.TableColumn(schema);
				colvarClientid.ColumnName = "clientid";
				colvarClientid.DataType = DbType.String;
				colvarClientid.MaxLength = 64;
				colvarClientid.AutoIncrement = false;
				colvarClientid.IsNullable = false;
				colvarClientid.IsPrimaryKey = true;
				colvarClientid.IsForeignKey = false;
				colvarClientid.IsReadOnly = false;
				colvarClientid.DefaultSetting = @"";
				colvarClientid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarClientid);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["AceMail"].AddSchema("eventclients",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Eventid")]
		[Bindable(true)]
		public string Eventid 
		{
			get { return GetColumnValue<string>(Columns.Eventid); }
			set { SetColumnValue(Columns.Eventid, value); }
		}
		  
		[XmlAttribute("Clientid")]
		[Bindable(true)]
		public string Clientid 
		{
			get { return GetColumnValue<string>(Columns.Clientid); }
			set { SetColumnValue(Columns.Clientid, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varEventid,string varClientid)
		{
			Eventclient item = new Eventclient();
			
			item.Eventid = varEventid;
			
			item.Clientid = varClientid;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varEventid,string varClientid)
		{
			Eventclient item = new Eventclient();
			
				item.Eventid = varEventid;
			
				item.Clientid = varClientid;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn EventidColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ClientidColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Eventid = @"eventid";
			 public static string Clientid = @"clientid";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
