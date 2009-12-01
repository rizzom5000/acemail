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
	/// Strongly-typed collection for the Eml class.
	/// </summary>
    [Serializable]
	public partial class EmlCollection : ActiveList<Eml, EmlCollection>
	{	   
		public EmlCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>EmlCollection</returns>
		public EmlCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Eml o = this[i];
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
	/// This is an ActiveRecord class which wraps the eml table.
	/// </summary>
	[Serializable]
	public partial class Eml : ActiveRecord<Eml>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Eml()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Eml(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Eml(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Eml(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("eml", TableType.Table, DataService.GetInstance("AceMail"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"";
				//columns
				
				TableSchema.TableColumn colvarEmlid = new TableSchema.TableColumn(schema);
				colvarEmlid.ColumnName = "emlid";
				colvarEmlid.DataType = DbType.String;
				colvarEmlid.MaxLength = 64;
				colvarEmlid.AutoIncrement = false;
				colvarEmlid.IsNullable = false;
				colvarEmlid.IsPrimaryKey = true;
				colvarEmlid.IsForeignKey = false;
				colvarEmlid.IsReadOnly = false;
				colvarEmlid.DefaultSetting = @"";
				colvarEmlid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmlid);
				
				TableSchema.TableColumn colvarEmlpath = new TableSchema.TableColumn(schema);
				colvarEmlpath.ColumnName = "emlpath";
				colvarEmlpath.DataType = DbType.String;
				colvarEmlpath.MaxLength = 50;
				colvarEmlpath.AutoIncrement = false;
				colvarEmlpath.IsNullable = false;
				colvarEmlpath.IsPrimaryKey = false;
				colvarEmlpath.IsForeignKey = false;
				colvarEmlpath.IsReadOnly = false;
				colvarEmlpath.DefaultSetting = @"";
				colvarEmlpath.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmlpath);
				
				TableSchema.TableColumn colvarSubject = new TableSchema.TableColumn(schema);
				colvarSubject.ColumnName = "subject";
				colvarSubject.DataType = DbType.String;
				colvarSubject.MaxLength = 50;
				colvarSubject.AutoIncrement = false;
				colvarSubject.IsNullable = true;
				colvarSubject.IsPrimaryKey = false;
				colvarSubject.IsForeignKey = false;
				colvarSubject.IsReadOnly = false;
				colvarSubject.DefaultSetting = @"";
				colvarSubject.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSubject);
				
				TableSchema.TableColumn colvarFromaddress = new TableSchema.TableColumn(schema);
				colvarFromaddress.ColumnName = "fromaddress";
				colvarFromaddress.DataType = DbType.String;
				colvarFromaddress.MaxLength = 50;
				colvarFromaddress.AutoIncrement = false;
				colvarFromaddress.IsNullable = true;
				colvarFromaddress.IsPrimaryKey = false;
				colvarFromaddress.IsForeignKey = false;
				colvarFromaddress.IsReadOnly = false;
				colvarFromaddress.DefaultSetting = @"";
				colvarFromaddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFromaddress);
				
				TableSchema.TableColumn colvarType = new TableSchema.TableColumn(schema);
				colvarType.ColumnName = "type";
				colvarType.DataType = DbType.Int32;
				colvarType.MaxLength = 10;
				colvarType.AutoIncrement = false;
				colvarType.IsNullable = true;
				colvarType.IsPrimaryKey = false;
				colvarType.IsForeignKey = false;
				colvarType.IsReadOnly = false;
				colvarType.DefaultSetting = @"";
				colvarType.ForeignKeyTableName = "";
				schema.Columns.Add(colvarType);
				
				TableSchema.TableColumn colvarAnswered = new TableSchema.TableColumn(schema);
				colvarAnswered.ColumnName = "answered";
				colvarAnswered.DataType = DbType.Boolean;
				colvarAnswered.MaxLength = 1;
				colvarAnswered.AutoIncrement = false;
				colvarAnswered.IsNullable = true;
				colvarAnswered.IsPrimaryKey = false;
				colvarAnswered.IsForeignKey = false;
				colvarAnswered.IsReadOnly = false;
				colvarAnswered.DefaultSetting = @"";
				colvarAnswered.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAnswered);
				
				TableSchema.TableColumn colvarSeen = new TableSchema.TableColumn(schema);
				colvarSeen.ColumnName = "seen";
				colvarSeen.DataType = DbType.Boolean;
				colvarSeen.MaxLength = 1;
				colvarSeen.AutoIncrement = false;
				colvarSeen.IsNullable = true;
				colvarSeen.IsPrimaryKey = false;
				colvarSeen.IsForeignKey = false;
				colvarSeen.IsReadOnly = false;
				colvarSeen.DefaultSetting = @"";
				colvarSeen.ForeignKeyTableName = "";
				schema.Columns.Add(colvarSeen);
				
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
				DataService.Providers["AceMail"].AddSchema("eml",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Emlid")]
		[Bindable(true)]
		public string Emlid 
		{
			get { return GetColumnValue<string>(Columns.Emlid); }
			set { SetColumnValue(Columns.Emlid, value); }
		}
		  
		[XmlAttribute("Emlpath")]
		[Bindable(true)]
		public string Emlpath 
		{
			get { return GetColumnValue<string>(Columns.Emlpath); }
			set { SetColumnValue(Columns.Emlpath, value); }
		}
		  
		[XmlAttribute("Subject")]
		[Bindable(true)]
		public string Subject 
		{
			get { return GetColumnValue<string>(Columns.Subject); }
			set { SetColumnValue(Columns.Subject, value); }
		}
		  
		[XmlAttribute("Fromaddress")]
		[Bindable(true)]
		public string Fromaddress 
		{
			get { return GetColumnValue<string>(Columns.Fromaddress); }
			set { SetColumnValue(Columns.Fromaddress, value); }
		}
		  
		[XmlAttribute("Type")]
		[Bindable(true)]
		public int? Type 
		{
			get { return GetColumnValue<int?>(Columns.Type); }
			set { SetColumnValue(Columns.Type, value); }
		}
		  
		[XmlAttribute("Answered")]
		[Bindable(true)]
		public bool? Answered 
		{
			get { return GetColumnValue<bool?>(Columns.Answered); }
			set { SetColumnValue(Columns.Answered, value); }
		}
		  
		[XmlAttribute("Seen")]
		[Bindable(true)]
		public bool? Seen 
		{
			get { return GetColumnValue<bool?>(Columns.Seen); }
			set { SetColumnValue(Columns.Seen, value); }
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
		public static void Insert(string varEmlid,string varEmlpath,string varSubject,string varFromaddress,int? varType,bool? varAnswered,bool? varSeen,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Eml item = new Eml();
			
			item.Emlid = varEmlid;
			
			item.Emlpath = varEmlpath;
			
			item.Subject = varSubject;
			
			item.Fromaddress = varFromaddress;
			
			item.Type = varType;
			
			item.Answered = varAnswered;
			
			item.Seen = varSeen;
			
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
		public static void Update(string varEmlid,string varEmlpath,string varSubject,string varFromaddress,int? varType,bool? varAnswered,bool? varSeen,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Eml item = new Eml();
			
				item.Emlid = varEmlid;
			
				item.Emlpath = varEmlpath;
			
				item.Subject = varSubject;
			
				item.Fromaddress = varFromaddress;
			
				item.Type = varType;
			
				item.Answered = varAnswered;
			
				item.Seen = varSeen;
			
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
        
        
        public static TableSchema.TableColumn EmlidColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn EmlpathColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn SubjectColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn FromaddressColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn TypeColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn AnsweredColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn SeenColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateddateColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateddateColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Emlid = @"emlid";
			 public static string Emlpath = @"emlpath";
			 public static string Subject = @"subject";
			 public static string Fromaddress = @"fromaddress";
			 public static string Type = @"type";
			 public static string Answered = @"answered";
			 public static string Seen = @"seen";
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
