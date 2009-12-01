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
	/// Strongly-typed collection for the Group class.
	/// </summary>
    [Serializable]
	public partial class GroupCollection : ActiveList<Group, GroupCollection>
	{	   
		public GroupCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>GroupCollection</returns>
		public GroupCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Group o = this[i];
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
	/// This is an ActiveRecord class which wraps the group table.
	/// </summary>
	[Serializable]
	public partial class Group : ActiveRecord<Group>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Group()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Group(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Group(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Group(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("group", TableType.Table, DataService.GetInstance("AceMail"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"";
				//columns
				
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
				
				TableSchema.TableColumn colvarGroupName = new TableSchema.TableColumn(schema);
				colvarGroupName.ColumnName = "GroupName";
				colvarGroupName.DataType = DbType.String;
				colvarGroupName.MaxLength = 50;
				colvarGroupName.AutoIncrement = false;
				colvarGroupName.IsNullable = false;
				colvarGroupName.IsPrimaryKey = false;
				colvarGroupName.IsForeignKey = false;
				colvarGroupName.IsReadOnly = false;
				colvarGroupName.DefaultSetting = @"";
				colvarGroupName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarGroupName);
				
				TableSchema.TableColumn colvarNotes = new TableSchema.TableColumn(schema);
				colvarNotes.ColumnName = "Notes";
				colvarNotes.DataType = DbType.String;
				colvarNotes.MaxLength = 5000;
				colvarNotes.AutoIncrement = false;
				colvarNotes.IsNullable = true;
				colvarNotes.IsPrimaryKey = false;
				colvarNotes.IsForeignKey = false;
				colvarNotes.IsReadOnly = false;
				colvarNotes.DefaultSetting = @"";
				colvarNotes.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNotes);
				
				TableSchema.TableColumn colvarCreateddate = new TableSchema.TableColumn(schema);
				colvarCreateddate.ColumnName = "createddate";
				colvarCreateddate.DataType = DbType.DateTime;
				colvarCreateddate.MaxLength = 0;
				colvarCreateddate.AutoIncrement = false;
				colvarCreateddate.IsNullable = false;
				colvarCreateddate.IsPrimaryKey = false;
				colvarCreateddate.IsForeignKey = false;
				colvarCreateddate.IsReadOnly = false;
				colvarCreateddate.DefaultSetting = @"";
				colvarCreateddate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCreateddate);
				
				TableSchema.TableColumn colvarUpdateddate = new TableSchema.TableColumn(schema);
				colvarUpdateddate.ColumnName = "updateddate";
				colvarUpdateddate.DataType = DbType.DateTime;
				colvarUpdateddate.MaxLength = 0;
				colvarUpdateddate.AutoIncrement = false;
				colvarUpdateddate.IsNullable = false;
				colvarUpdateddate.IsPrimaryKey = false;
				colvarUpdateddate.IsForeignKey = false;
				colvarUpdateddate.IsReadOnly = false;
				colvarUpdateddate.DefaultSetting = @"";
				colvarUpdateddate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUpdateddate);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["AceMail"].AddSchema("group",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Groupid")]
		[Bindable(true)]
		public string Groupid 
		{
			get { return GetColumnValue<string>(Columns.Groupid); }
			set { SetColumnValue(Columns.Groupid, value); }
		}
		  
		[XmlAttribute("GroupName")]
		[Bindable(true)]
		public string GroupName 
		{
			get { return GetColumnValue<string>(Columns.GroupName); }
			set { SetColumnValue(Columns.GroupName, value); }
		}
		  
		[XmlAttribute("Notes")]
		[Bindable(true)]
		public string Notes 
		{
			get { return GetColumnValue<string>(Columns.Notes); }
			set { SetColumnValue(Columns.Notes, value); }
		}
		  
		[XmlAttribute("Createddate")]
		[Bindable(true)]
		public DateTime Createddate 
		{
			get { return GetColumnValue<DateTime>(Columns.Createddate); }
			set { SetColumnValue(Columns.Createddate, value); }
		}
		  
		[XmlAttribute("Updateddate")]
		[Bindable(true)]
		public DateTime Updateddate 
		{
			get { return GetColumnValue<DateTime>(Columns.Updateddate); }
			set { SetColumnValue(Columns.Updateddate, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varGroupid,string varGroupName,string varNotes,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Group item = new Group();
			
			item.Groupid = varGroupid;
			
			item.GroupName = varGroupName;
			
			item.Notes = varNotes;
			
			item.Createddate = varCreateddate;
			
			item.Updateddate = varUpdateddate;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varGroupid,string varGroupName,string varNotes,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Group item = new Group();
			
				item.Groupid = varGroupid;
			
				item.GroupName = varGroupName;
			
				item.Notes = varNotes;
			
				item.Createddate = varCreateddate;
			
				item.Updateddate = varUpdateddate;
			
			item.IsNew = false;
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		#endregion
        
        
        
        #region Typed Columns
        
        
        public static TableSchema.TableColumn GroupidColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn GroupNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn NotesColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateddateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateddateColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Groupid = @"groupid";
			 public static string GroupName = @"GroupName";
			 public static string Notes = @"Notes";
			 public static string Createddate = @"createddate";
			 public static string Updateddate = @"updateddate";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
