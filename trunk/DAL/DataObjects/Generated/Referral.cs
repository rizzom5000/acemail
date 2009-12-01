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
	/// Strongly-typed collection for the Referral class.
	/// </summary>
    [Serializable]
	public partial class ReferralCollection : ActiveList<Referral, ReferralCollection>
	{	   
		public ReferralCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ReferralCollection</returns>
		public ReferralCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Referral o = this[i];
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
	/// This is an ActiveRecord class which wraps the referral table.
	/// </summary>
	[Serializable]
	public partial class Referral : ActiveRecord<Referral>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Referral()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Referral(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Referral(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Referral(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("referral", TableType.Table, DataService.GetInstance("AceMail"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"";
				//columns
				
				TableSchema.TableColumn colvarReferralid = new TableSchema.TableColumn(schema);
				colvarReferralid.ColumnName = "referralid";
				colvarReferralid.DataType = DbType.String;
				colvarReferralid.MaxLength = 64;
				colvarReferralid.AutoIncrement = false;
				colvarReferralid.IsNullable = false;
				colvarReferralid.IsPrimaryKey = true;
				colvarReferralid.IsForeignKey = false;
				colvarReferralid.IsReadOnly = false;
				colvarReferralid.DefaultSetting = @"";
				colvarReferralid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReferralid);
				
				TableSchema.TableColumn colvarReferredClientID = new TableSchema.TableColumn(schema);
				colvarReferredClientID.ColumnName = "referredClientID";
				colvarReferredClientID.DataType = DbType.String;
				colvarReferredClientID.MaxLength = 64;
				colvarReferredClientID.AutoIncrement = false;
				colvarReferredClientID.IsNullable = false;
				colvarReferredClientID.IsPrimaryKey = false;
				colvarReferredClientID.IsForeignKey = false;
				colvarReferredClientID.IsReadOnly = false;
				colvarReferredClientID.DefaultSetting = @"";
				colvarReferredClientID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReferredClientID);
				
				TableSchema.TableColumn colvarReferredByID = new TableSchema.TableColumn(schema);
				colvarReferredByID.ColumnName = "referredByID";
				colvarReferredByID.DataType = DbType.String;
				colvarReferredByID.MaxLength = 64;
				colvarReferredByID.AutoIncrement = false;
				colvarReferredByID.IsNullable = false;
				colvarReferredByID.IsPrimaryKey = false;
				colvarReferredByID.IsForeignKey = false;
				colvarReferredByID.IsReadOnly = false;
				colvarReferredByID.DefaultSetting = @"";
				colvarReferredByID.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReferredByID);
				
				TableSchema.TableColumn colvarReferralDate = new TableSchema.TableColumn(schema);
				colvarReferralDate.ColumnName = "ReferralDate";
				colvarReferralDate.DataType = DbType.DateTime;
				colvarReferralDate.MaxLength = 0;
				colvarReferralDate.AutoIncrement = false;
				colvarReferralDate.IsNullable = false;
				colvarReferralDate.IsPrimaryKey = false;
				colvarReferralDate.IsForeignKey = false;
				colvarReferralDate.IsReadOnly = false;
				colvarReferralDate.DefaultSetting = @"";
				colvarReferralDate.ForeignKeyTableName = "";
				schema.Columns.Add(colvarReferralDate);
				
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
				DataService.Providers["AceMail"].AddSchema("referral",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Referralid")]
		[Bindable(true)]
		public string Referralid 
		{
			get { return GetColumnValue<string>(Columns.Referralid); }
			set { SetColumnValue(Columns.Referralid, value); }
		}
		  
		[XmlAttribute("ReferredClientID")]
		[Bindable(true)]
		public string ReferredClientID 
		{
			get { return GetColumnValue<string>(Columns.ReferredClientID); }
			set { SetColumnValue(Columns.ReferredClientID, value); }
		}
		  
		[XmlAttribute("ReferredByID")]
		[Bindable(true)]
		public string ReferredByID 
		{
			get { return GetColumnValue<string>(Columns.ReferredByID); }
			set { SetColumnValue(Columns.ReferredByID, value); }
		}
		  
		[XmlAttribute("ReferralDate")]
		[Bindable(true)]
		public DateTime ReferralDate 
		{
			get { return GetColumnValue<DateTime>(Columns.ReferralDate); }
			set { SetColumnValue(Columns.ReferralDate, value); }
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
		public static void Insert(string varReferralid,string varReferredClientID,string varReferredByID,DateTime varReferralDate,string varNotes,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Referral item = new Referral();
			
			item.Referralid = varReferralid;
			
			item.ReferredClientID = varReferredClientID;
			
			item.ReferredByID = varReferredByID;
			
			item.ReferralDate = varReferralDate;
			
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
		public static void Update(string varReferralid,string varReferredClientID,string varReferredByID,DateTime varReferralDate,string varNotes,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Referral item = new Referral();
			
				item.Referralid = varReferralid;
			
				item.ReferredClientID = varReferredClientID;
			
				item.ReferredByID = varReferredByID;
			
				item.ReferralDate = varReferralDate;
			
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
        
        
        public static TableSchema.TableColumn ReferralidColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn ReferredClientIDColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ReferredByIDColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ReferralDateColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn NotesColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateddateColumn
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateddateColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Referralid = @"referralid";
			 public static string ReferredClientID = @"referredClientID";
			 public static string ReferredByID = @"referredByID";
			 public static string ReferralDate = @"ReferralDate";
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
