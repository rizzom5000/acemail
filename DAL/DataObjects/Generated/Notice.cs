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
	/// Strongly-typed collection for the Notice class.
	/// </summary>
    [Serializable]
	public partial class NoticeCollection : ActiveList<Notice, NoticeCollection>
	{	   
		public NoticeCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NoticeCollection</returns>
		public NoticeCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Notice o = this[i];
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
	/// This is an ActiveRecord class which wraps the notice table.
	/// </summary>
	[Serializable]
	public partial class Notice : ActiveRecord<Notice>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Notice()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Notice(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Notice(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Notice(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("notice", TableType.Table, DataService.GetInstance("AceMail"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"";
				//columns
				
				TableSchema.TableColumn colvarNoticeid = new TableSchema.TableColumn(schema);
				colvarNoticeid.ColumnName = "noticeid";
				colvarNoticeid.DataType = DbType.String;
				colvarNoticeid.MaxLength = 64;
				colvarNoticeid.AutoIncrement = false;
				colvarNoticeid.IsNullable = false;
				colvarNoticeid.IsPrimaryKey = true;
				colvarNoticeid.IsForeignKey = false;
				colvarNoticeid.IsReadOnly = false;
				colvarNoticeid.DefaultSetting = @"";
				colvarNoticeid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNoticeid);
				
				TableSchema.TableColumn colvarName = new TableSchema.TableColumn(schema);
				colvarName.ColumnName = "name";
				colvarName.DataType = DbType.String;
				colvarName.MaxLength = 50;
				colvarName.AutoIncrement = false;
				colvarName.IsNullable = true;
				colvarName.IsPrimaryKey = false;
				colvarName.IsForeignKey = false;
				colvarName.IsReadOnly = false;
				colvarName.DefaultSetting = @"";
				colvarName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarName);
				
				TableSchema.TableColumn colvarUserid = new TableSchema.TableColumn(schema);
				colvarUserid.ColumnName = "userid";
				colvarUserid.DataType = DbType.String;
				colvarUserid.MaxLength = 64;
				colvarUserid.AutoIncrement = false;
				colvarUserid.IsNullable = false;
				colvarUserid.IsPrimaryKey = false;
				colvarUserid.IsForeignKey = false;
				colvarUserid.IsReadOnly = false;
				colvarUserid.DefaultSetting = @"";
				colvarUserid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserid);
				
				TableSchema.TableColumn colvarType = new TableSchema.TableColumn(schema);
				colvarType.ColumnName = "type";
				colvarType.DataType = DbType.Int32;
				colvarType.MaxLength = 3;
				colvarType.AutoIncrement = false;
				colvarType.IsNullable = false;
				colvarType.IsPrimaryKey = false;
				colvarType.IsForeignKey = false;
				colvarType.IsReadOnly = false;
				colvarType.DefaultSetting = @"";
				colvarType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarType);
				
				TableSchema.TableColumn colvarNoticedate = new TableSchema.TableColumn(schema);
				colvarNoticedate.ColumnName = "noticedate";
				colvarNoticedate.DataType = DbType.DateTime;
				colvarNoticedate.MaxLength = 0;
				colvarNoticedate.AutoIncrement = false;
				colvarNoticedate.IsNullable = false;
				colvarNoticedate.IsPrimaryKey = false;
				colvarNoticedate.IsForeignKey = false;
				colvarNoticedate.IsReadOnly = false;
				colvarNoticedate.DefaultSetting = @"";
				colvarNoticedate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarNoticedate);
				
				TableSchema.TableColumn colvarNotes = new TableSchema.TableColumn(schema);
				colvarNotes.ColumnName = "notes";
				colvarNotes.DataType = DbType.String;
				colvarNotes.MaxLength = 5120;
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
				DataService.Providers["AceMail"].AddSchema("notice",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Noticeid")]
		[Bindable(true)]
		public string Noticeid 
		{
			get { return GetColumnValue<string>(Columns.Noticeid); }
			set { SetColumnValue(Columns.Noticeid, value); }
		}
		  
		[XmlAttribute("Name")]
		[Bindable(true)]
		public string Name 
		{
			get { return GetColumnValue<string>(Columns.Name); }
			set { SetColumnValue(Columns.Name, value); }
		}
		  
		[XmlAttribute("Userid")]
		[Bindable(true)]
		public string Userid 
		{
			get { return GetColumnValue<string>(Columns.Userid); }
			set { SetColumnValue(Columns.Userid, value); }
		}
		  
		[XmlAttribute("Type")]
		[Bindable(true)]
		public int Type 
		{
			get { return GetColumnValue<int>(Columns.Type); }
			set { SetColumnValue(Columns.Type, value); }
		}
		  
		[XmlAttribute("Noticedate")]
		[Bindable(true)]
		public DateTime Noticedate 
		{
			get { return GetColumnValue<DateTime>(Columns.Noticedate); }
			set { SetColumnValue(Columns.Noticedate, value); }
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
		public static void Insert(string varNoticeid,string varName,string varUserid,int varType,DateTime varNoticedate,string varNotes,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Notice item = new Notice();
			
			item.Noticeid = varNoticeid;
			
			item.Name = varName;
			
			item.Userid = varUserid;
			
			item.Type = varType;
			
			item.Noticedate = varNoticedate;
			
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
		public static void Update(string varNoticeid,string varName,string varUserid,int varType,DateTime varNoticedate,string varNotes,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Notice item = new Notice();
			
				item.Noticeid = varNoticeid;
			
				item.Name = varName;
			
				item.Userid = varUserid;
			
				item.Type = varType;
			
				item.Noticedate = varNoticedate;
			
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
        
        
        public static TableSchema.TableColumn NoticeidColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn NameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn UseridColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn TypeColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NoticedateColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn NotesColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateddateColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateddateColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Noticeid = @"noticeid";
			 public static string Name = @"name";
			 public static string Userid = @"userid";
			 public static string Type = @"type";
			 public static string Noticedate = @"noticedate";
			 public static string Notes = @"notes";
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
