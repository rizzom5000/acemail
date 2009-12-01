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
	/// Strongly-typed collection for the Eventgroup class.
	/// </summary>
    [Serializable]
	public partial class EventgroupCollection : ActiveList<Eventgroup, EventgroupCollection>
	{	   
		public EventgroupCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>EventgroupCollection</returns>
		public EventgroupCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Eventgroup o = this[i];
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
	/// This is an ActiveRecord class which wraps the eventgroups table.
	/// </summary>
	[Serializable]
	public partial class Eventgroup : ActiveRecord<Eventgroup>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Eventgroup()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Eventgroup(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Eventgroup(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Eventgroup(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("eventgroups", TableType.Table, DataService.GetInstance("AceMail"));
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
				
				TableSchema.TableColumn colvarGroupid = new TableSchema.TableColumn(schema);
				colvarGroupid.ColumnName = "groupid";
				colvarGroupid.DataType = DbType.String;
				colvarGroupid.MaxLength = 64;
				colvarGroupid.AutoIncrement = false;
				colvarGroupid.IsNullable = false;
				colvarGroupid.IsPrimaryKey = true;
				colvarGroupid.IsForeignKey = false;
				colvarGroupid.IsReadOnly = false;
				colvarGroupid.DefaultSetting = @"";
				colvarGroupid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGroupid);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["AceMail"].AddSchema("eventgroups",schema);
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
		  
		[XmlAttribute("Groupid")]
		[Bindable(true)]
		public string Groupid 
		{
			get { return GetColumnValue<string>(Columns.Groupid); }
			set { SetColumnValue(Columns.Groupid, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varEventid,string varGroupid)
		{
			Eventgroup item = new Eventgroup();
			
			item.Eventid = varEventid;
			
			item.Groupid = varGroupid;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varEventid,string varGroupid)
		{
			Eventgroup item = new Eventgroup();
			
				item.Eventid = varEventid;
			
				item.Groupid = varGroupid;
			
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
        
        
        
        public static TableSchema.TableColumn GroupidColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Eventid = @"eventid";
			 public static string Groupid = @"groupid";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
