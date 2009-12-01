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
	/// Strongly-typed collection for the User class.
	/// </summary>
    [Serializable]
	public partial class UserCollection : ActiveList<User, UserCollection>
	{	   
		public UserCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>UserCollection</returns>
		public UserCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                User o = this[i];
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
	/// This is an ActiveRecord class which wraps the user table.
	/// </summary>
	[Serializable]
	public partial class User : ActiveRecord<User>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public User()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public User(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public User(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public User(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("user", TableType.Table, DataService.GetInstance("AceMail"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"";
				//columns
				
				TableSchema.TableColumn colvarUserid = new TableSchema.TableColumn(schema);
				colvarUserid.ColumnName = "userid";
				colvarUserid.DataType = DbType.String;
				colvarUserid.MaxLength = 64;
				colvarUserid.AutoIncrement = false;
				colvarUserid.IsNullable = false;
				colvarUserid.IsPrimaryKey = true;
				colvarUserid.IsForeignKey = false;
				colvarUserid.IsReadOnly = false;
				colvarUserid.DefaultSetting = @"";
				colvarUserid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarUserid);
				
				TableSchema.TableColumn colvarLogin = new TableSchema.TableColumn(schema);
				colvarLogin.ColumnName = "Login";
				colvarLogin.DataType = DbType.String;
				colvarLogin.MaxLength = 50;
				colvarLogin.AutoIncrement = false;
				colvarLogin.IsNullable = false;
				colvarLogin.IsPrimaryKey = false;
				colvarLogin.IsForeignKey = false;
				colvarLogin.IsReadOnly = false;
				colvarLogin.DefaultSetting = @"";
				colvarLogin.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLogin);
				
				TableSchema.TableColumn colvarPassword = new TableSchema.TableColumn(schema);
				colvarPassword.ColumnName = "Password";
				colvarPassword.DataType = DbType.String;
				colvarPassword.MaxLength = 50;
				colvarPassword.AutoIncrement = false;
				colvarPassword.IsNullable = false;
				colvarPassword.IsPrimaryKey = false;
				colvarPassword.IsForeignKey = false;
				colvarPassword.IsReadOnly = false;
				colvarPassword.DefaultSetting = @"";
				colvarPassword.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPassword);
				
				TableSchema.TableColumn colvarContactEmail = new TableSchema.TableColumn(schema);
				colvarContactEmail.ColumnName = "ContactEmail";
				colvarContactEmail.DataType = DbType.String;
				colvarContactEmail.MaxLength = 50;
				colvarContactEmail.AutoIncrement = false;
				colvarContactEmail.IsNullable = false;
				colvarContactEmail.IsPrimaryKey = false;
				colvarContactEmail.IsForeignKey = false;
				colvarContactEmail.IsReadOnly = false;
				colvarContactEmail.DefaultSetting = @"";
				colvarContactEmail.ForeignKeyTableName = "";
				schema.Columns.Add(colvarContactEmail);
				
				TableSchema.TableColumn colvarFirstname = new TableSchema.TableColumn(schema);
				colvarFirstname.ColumnName = "firstname";
				colvarFirstname.DataType = DbType.String;
				colvarFirstname.MaxLength = 50;
				colvarFirstname.AutoIncrement = false;
				colvarFirstname.IsNullable = true;
				colvarFirstname.IsPrimaryKey = false;
				colvarFirstname.IsForeignKey = false;
				colvarFirstname.IsReadOnly = false;
				colvarFirstname.DefaultSetting = @"";
				colvarFirstname.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstname);
				
				TableSchema.TableColumn colvarLastname = new TableSchema.TableColumn(schema);
				colvarLastname.ColumnName = "lastname";
				colvarLastname.DataType = DbType.String;
				colvarLastname.MaxLength = 50;
				colvarLastname.AutoIncrement = false;
				colvarLastname.IsNullable = true;
				colvarLastname.IsPrimaryKey = false;
				colvarLastname.IsForeignKey = false;
				colvarLastname.IsReadOnly = false;
				colvarLastname.DefaultSetting = @"";
				colvarLastname.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastname);
				
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
				DataService.Providers["AceMail"].AddSchema("user",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Userid")]
		[Bindable(true)]
		public string Userid 
		{
			get { return GetColumnValue<string>(Columns.Userid); }
			set { SetColumnValue(Columns.Userid, value); }
		}
		  
		[XmlAttribute("Login")]
		[Bindable(true)]
		public string Login 
		{
			get { return GetColumnValue<string>(Columns.Login); }
			set { SetColumnValue(Columns.Login, value); }
		}
		  
		[XmlAttribute("Password")]
		[Bindable(true)]
		public string Password 
		{
			get { return GetColumnValue<string>(Columns.Password); }
			set { SetColumnValue(Columns.Password, value); }
		}
		  
		[XmlAttribute("ContactEmail")]
		[Bindable(true)]
		public string ContactEmail 
		{
			get { return GetColumnValue<string>(Columns.ContactEmail); }
			set { SetColumnValue(Columns.ContactEmail, value); }
		}
		  
		[XmlAttribute("Firstname")]
		[Bindable(true)]
		public string Firstname 
		{
			get { return GetColumnValue<string>(Columns.Firstname); }
			set { SetColumnValue(Columns.Firstname, value); }
		}
		  
		[XmlAttribute("Lastname")]
		[Bindable(true)]
		public string Lastname 
		{
			get { return GetColumnValue<string>(Columns.Lastname); }
			set { SetColumnValue(Columns.Lastname, value); }
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
		public static void Insert(string varUserid,string varLogin,string varPassword,string varContactEmail,string varFirstname,string varLastname,DateTime varCreateddate,DateTime varUpdateddate)
		{
			User item = new User();
			
			item.Userid = varUserid;
			
			item.Login = varLogin;
			
			item.Password = varPassword;
			
			item.ContactEmail = varContactEmail;
			
			item.Firstname = varFirstname;
			
			item.Lastname = varLastname;
			
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
		public static void Update(string varUserid,string varLogin,string varPassword,string varContactEmail,string varFirstname,string varLastname,DateTime varCreateddate,DateTime varUpdateddate)
		{
			User item = new User();
			
				item.Userid = varUserid;
			
				item.Login = varLogin;
			
				item.Password = varPassword;
			
				item.ContactEmail = varContactEmail;
			
				item.Firstname = varFirstname;
			
				item.Lastname = varLastname;
			
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
        
        
        public static TableSchema.TableColumn UseridColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn LoginColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn PasswordColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ContactEmailColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn FirstnameColumn
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn LastnameColumn
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
			 public static string Userid = @"userid";
			 public static string Login = @"Login";
			 public static string Password = @"Password";
			 public static string ContactEmail = @"ContactEmail";
			 public static string Firstname = @"firstname";
			 public static string Lastname = @"lastname";
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
