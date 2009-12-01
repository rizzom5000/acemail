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
	/// Strongly-typed collection for the File class.
	/// </summary>
    [Serializable]
	public partial class FileCollection : ActiveList<File, FileCollection>
	{	   
		public FileCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>FileCollection</returns>
		public FileCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                File o = this[i];
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
	/// This is an ActiveRecord class which wraps the file table.
	/// </summary>
	[Serializable]
	public partial class File : ActiveRecord<File>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public File()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public File(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public File(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public File(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("file", TableType.Table, DataService.GetInstance("AceMail"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"";
				//columns
				
				TableSchema.TableColumn colvarFileid = new TableSchema.TableColumn(schema);
				colvarFileid.ColumnName = "fileid";
				colvarFileid.DataType = DbType.String;
				colvarFileid.MaxLength = 64;
				colvarFileid.AutoIncrement = false;
				colvarFileid.IsNullable = false;
				colvarFileid.IsPrimaryKey = true;
				colvarFileid.IsForeignKey = false;
				colvarFileid.IsReadOnly = false;
				colvarFileid.DefaultSetting = @"";
				colvarFileid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFileid);
				
				TableSchema.TableColumn colvarFilename = new TableSchema.TableColumn(schema);
				colvarFilename.ColumnName = "filename";
				colvarFilename.DataType = DbType.String;
				colvarFilename.MaxLength = 50;
				colvarFilename.AutoIncrement = false;
				colvarFilename.IsNullable = true;
				colvarFilename.IsPrimaryKey = false;
				colvarFilename.IsForeignKey = false;
				colvarFilename.IsReadOnly = false;
				colvarFilename.DefaultSetting = @"";
				colvarFilename.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFilename);
				
				TableSchema.TableColumn colvarExtension = new TableSchema.TableColumn(schema);
				colvarExtension.ColumnName = "extension";
				colvarExtension.DataType = DbType.String;
				colvarExtension.MaxLength = 4;
				colvarExtension.AutoIncrement = false;
				colvarExtension.IsNullable = false;
				colvarExtension.IsPrimaryKey = false;
				colvarExtension.IsForeignKey = false;
				colvarExtension.IsReadOnly = false;
				colvarExtension.DefaultSetting = @"";
				colvarExtension.ForeignKeyTableName = "";
				schema.Columns.Add(colvarExtension);
				
				TableSchema.TableColumn colvarAddress = new TableSchema.TableColumn(schema);
				colvarAddress.ColumnName = "address";
				colvarAddress.DataType = DbType.String;
				colvarAddress.MaxLength = 500;
				colvarAddress.AutoIncrement = false;
				colvarAddress.IsNullable = false;
				colvarAddress.IsPrimaryKey = false;
				colvarAddress.IsForeignKey = false;
				colvarAddress.IsReadOnly = false;
				colvarAddress.DefaultSetting = @"";
				colvarAddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddress);
				
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
				DataService.Providers["AceMail"].AddSchema("file",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Fileid")]
		[Bindable(true)]
		public string Fileid 
		{
			get { return GetColumnValue<string>(Columns.Fileid); }
			set { SetColumnValue(Columns.Fileid, value); }
		}
		  
		[XmlAttribute("Filename")]
		[Bindable(true)]
		public string Filename 
		{
			get { return GetColumnValue<string>(Columns.Filename); }
			set { SetColumnValue(Columns.Filename, value); }
		}
		  
		[XmlAttribute("Extension")]
		[Bindable(true)]
		public string Extension 
		{
			get { return GetColumnValue<string>(Columns.Extension); }
			set { SetColumnValue(Columns.Extension, value); }
		}
		  
		[XmlAttribute("Address")]
		[Bindable(true)]
		public string Address 
		{
			get { return GetColumnValue<string>(Columns.Address); }
			set { SetColumnValue(Columns.Address, value); }
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
		public static void Insert(string varFileid,string varFilename,string varExtension,string varAddress,DateTime varCreateddate,DateTime varUpdateddate)
		{
			File item = new File();
			
			item.Fileid = varFileid;
			
			item.Filename = varFilename;
			
			item.Extension = varExtension;
			
			item.Address = varAddress;
			
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
		public static void Update(string varFileid,string varFilename,string varExtension,string varAddress,DateTime varCreateddate,DateTime varUpdateddate)
		{
			File item = new File();
			
				item.Fileid = varFileid;
			
				item.Filename = varFilename;
			
				item.Extension = varExtension;
			
				item.Address = varAddress;
			
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
        
        
        public static TableSchema.TableColumn FileidColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn FilenameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ExtensionColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn AddressColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateddateColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateddateColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Fileid = @"fileid";
			 public static string Filename = @"filename";
			 public static string Extension = @"extension";
			 public static string Address = @"address";
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
