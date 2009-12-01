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
	/// Strongly-typed collection for the Email class.
	/// </summary>
    [Serializable]
	public partial class EmailCollection : ActiveList<Email, EmailCollection>
	{	   
		public EmailCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>EmailCollection</returns>
		public EmailCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Email o = this[i];
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
	/// This is an ActiveRecord class which wraps the email table.
	/// </summary>
	[Serializable]
	public partial class Email : ActiveRecord<Email>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Email()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Email(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Email(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Email(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("email", TableType.Table, DataService.GetInstance("AceMail"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"";
				//columns
				
				TableSchema.TableColumn colvarEmailid = new TableSchema.TableColumn(schema);
				colvarEmailid.ColumnName = "emailid";
				colvarEmailid.DataType = DbType.String;
				colvarEmailid.MaxLength = 64;
				colvarEmailid.AutoIncrement = false;
				colvarEmailid.IsNullable = false;
				colvarEmailid.IsPrimaryKey = true;
				colvarEmailid.IsForeignKey = false;
				colvarEmailid.IsReadOnly = false;
				colvarEmailid.DefaultSetting = @"";
				colvarEmailid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmailid);
				
				TableSchema.TableColumn colvarOwnerid = new TableSchema.TableColumn(schema);
				colvarOwnerid.ColumnName = "ownerid";
				colvarOwnerid.DataType = DbType.String;
				colvarOwnerid.MaxLength = 64;
				colvarOwnerid.AutoIncrement = false;
				colvarOwnerid.IsNullable = false;
				colvarOwnerid.IsPrimaryKey = false;
				colvarOwnerid.IsForeignKey = false;
				colvarOwnerid.IsReadOnly = false;
				colvarOwnerid.DefaultSetting = @"";
				colvarOwnerid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarOwnerid);
				
				TableSchema.TableColumn colvarEmailaddress = new TableSchema.TableColumn(schema);
				colvarEmailaddress.ColumnName = "emailaddress";
				colvarEmailaddress.DataType = DbType.String;
				colvarEmailaddress.MaxLength = 50;
				colvarEmailaddress.AutoIncrement = false;
				colvarEmailaddress.IsNullable = true;
				colvarEmailaddress.IsPrimaryKey = false;
				colvarEmailaddress.IsForeignKey = false;
				colvarEmailaddress.IsReadOnly = false;
				colvarEmailaddress.DefaultSetting = @"";
				colvarEmailaddress.ForeignKeyTableName = "";
				schema.Columns.Add(colvarEmailaddress);
				
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
				DataService.Providers["AceMail"].AddSchema("email",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Emailid")]
		[Bindable(true)]
		public string Emailid 
		{
			get { return GetColumnValue<string>(Columns.Emailid); }
			set { SetColumnValue(Columns.Emailid, value); }
		}
		  
		[XmlAttribute("Ownerid")]
		[Bindable(true)]
		public string Ownerid 
		{
			get { return GetColumnValue<string>(Columns.Ownerid); }
			set { SetColumnValue(Columns.Ownerid, value); }
		}
		  
		[XmlAttribute("Emailaddress")]
		[Bindable(true)]
		public string Emailaddress 
		{
			get { return GetColumnValue<string>(Columns.Emailaddress); }
			set { SetColumnValue(Columns.Emailaddress, value); }
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
		public static void Insert(string varEmailid,string varOwnerid,string varEmailaddress,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Email item = new Email();
			
			item.Emailid = varEmailid;
			
			item.Ownerid = varOwnerid;
			
			item.Emailaddress = varEmailaddress;
			
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
		public static void Update(string varEmailid,string varOwnerid,string varEmailaddress,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Email item = new Email();
			
				item.Emailid = varEmailid;
			
				item.Ownerid = varOwnerid;
			
				item.Emailaddress = varEmailaddress;
			
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
        
        
        public static TableSchema.TableColumn EmailidColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn OwneridColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn EmailaddressColumn
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
			 public static string Emailid = @"emailid";
			 public static string Ownerid = @"ownerid";
			 public static string Emailaddress = @"emailaddress";
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
