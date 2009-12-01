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
	/// Strongly-typed collection for the Client class.
	/// </summary>
    [Serializable]
	public partial class ClientCollection : ActiveList<Client, ClientCollection>
	{	   
		public ClientCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ClientCollection</returns>
		public ClientCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Client o = this[i];
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
	/// This is an ActiveRecord class which wraps the client table.
	/// </summary>
	[Serializable]
	public partial class Client : ActiveRecord<Client>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Client()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Client(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Client(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Client(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("client", TableType.Table, DataService.GetInstance("AceMail"));
				schema.Columns = new TableSchema.TableColumnCollection();
				schema.SchemaName = @"";
				//columns
				
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
				
				TableSchema.TableColumn colvarFirstName = new TableSchema.TableColumn(schema);
				colvarFirstName.ColumnName = "FirstName";
				colvarFirstName.DataType = DbType.String;
				colvarFirstName.MaxLength = 50;
				colvarFirstName.AutoIncrement = false;
				colvarFirstName.IsNullable = false;
				colvarFirstName.IsPrimaryKey = false;
				colvarFirstName.IsForeignKey = false;
				colvarFirstName.IsReadOnly = false;
				colvarFirstName.DefaultSetting = @"";
				colvarFirstName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarFirstName);
				
				TableSchema.TableColumn colvarMiddleName = new TableSchema.TableColumn(schema);
				colvarMiddleName.ColumnName = "MiddleName";
				colvarMiddleName.DataType = DbType.String;
				colvarMiddleName.MaxLength = 50;
				colvarMiddleName.AutoIncrement = false;
				colvarMiddleName.IsNullable = true;
				colvarMiddleName.IsPrimaryKey = false;
				colvarMiddleName.IsForeignKey = false;
				colvarMiddleName.IsReadOnly = false;
				colvarMiddleName.DefaultSetting = @"";
				colvarMiddleName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarMiddleName);
				
				TableSchema.TableColumn colvarLastName = new TableSchema.TableColumn(schema);
				colvarLastName.ColumnName = "LastName";
				colvarLastName.DataType = DbType.String;
				colvarLastName.MaxLength = 50;
				colvarLastName.AutoIncrement = false;
				colvarLastName.IsNullable = false;
				colvarLastName.IsPrimaryKey = false;
				colvarLastName.IsForeignKey = false;
				colvarLastName.IsReadOnly = false;
				colvarLastName.DefaultSetting = @"";
				colvarLastName.ForeignKeyTableName = "";
				schema.Columns.Add(colvarLastName);
				
				TableSchema.TableColumn colvarAddress1 = new TableSchema.TableColumn(schema);
				colvarAddress1.ColumnName = "Address1";
				colvarAddress1.DataType = DbType.String;
				colvarAddress1.MaxLength = 50;
				colvarAddress1.AutoIncrement = false;
				colvarAddress1.IsNullable = true;
				colvarAddress1.IsPrimaryKey = false;
				colvarAddress1.IsForeignKey = false;
				colvarAddress1.IsReadOnly = false;
				colvarAddress1.DefaultSetting = @"";
				colvarAddress1.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddress1);
				
				TableSchema.TableColumn colvarAddress2 = new TableSchema.TableColumn(schema);
				colvarAddress2.ColumnName = "Address2";
				colvarAddress2.DataType = DbType.String;
				colvarAddress2.MaxLength = 50;
				colvarAddress2.AutoIncrement = false;
				colvarAddress2.IsNullable = true;
				colvarAddress2.IsPrimaryKey = false;
				colvarAddress2.IsForeignKey = false;
				colvarAddress2.IsReadOnly = false;
				colvarAddress2.DefaultSetting = @"";
				colvarAddress2.ForeignKeyTableName = "";
				schema.Columns.Add(colvarAddress2);
				
				TableSchema.TableColumn colvarCity = new TableSchema.TableColumn(schema);
				colvarCity.ColumnName = "City";
				colvarCity.DataType = DbType.String;
				colvarCity.MaxLength = 50;
				colvarCity.AutoIncrement = false;
				colvarCity.IsNullable = true;
				colvarCity.IsPrimaryKey = false;
				colvarCity.IsForeignKey = false;
				colvarCity.IsReadOnly = false;
				colvarCity.DefaultSetting = @"";
				colvarCity.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCity);
				
				TableSchema.TableColumn colvarStateCode = new TableSchema.TableColumn(schema);
				colvarStateCode.ColumnName = "StateCode";
				colvarStateCode.DataType = DbType.String;
				colvarStateCode.MaxLength = 2;
				colvarStateCode.AutoIncrement = false;
				colvarStateCode.IsNullable = true;
				colvarStateCode.IsPrimaryKey = false;
				colvarStateCode.IsForeignKey = false;
				colvarStateCode.IsReadOnly = false;
				colvarStateCode.DefaultSetting = @"";
				colvarStateCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarStateCode);
				
				TableSchema.TableColumn colvarPostalCode = new TableSchema.TableColumn(schema);
				colvarPostalCode.ColumnName = "PostalCode";
				colvarPostalCode.DataType = DbType.String;
				colvarPostalCode.MaxLength = 10;
				colvarPostalCode.AutoIncrement = false;
				colvarPostalCode.IsNullable = true;
				colvarPostalCode.IsPrimaryKey = false;
				colvarPostalCode.IsForeignKey = false;
				colvarPostalCode.IsReadOnly = false;
				colvarPostalCode.DefaultSetting = @"";
				colvarPostalCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPostalCode);
				
				TableSchema.TableColumn colvarCountryCode = new TableSchema.TableColumn(schema);
				colvarCountryCode.ColumnName = "CountryCode";
				colvarCountryCode.DataType = DbType.String;
				colvarCountryCode.MaxLength = 3;
				colvarCountryCode.AutoIncrement = false;
				colvarCountryCode.IsNullable = true;
				colvarCountryCode.IsPrimaryKey = false;
				colvarCountryCode.IsForeignKey = false;
				colvarCountryCode.IsReadOnly = false;
				colvarCountryCode.DefaultSetting = @"";
				colvarCountryCode.ForeignKeyTableName = "";
				schema.Columns.Add(colvarCountryCode);
				
				TableSchema.TableColumn colvarPhonePrimary = new TableSchema.TableColumn(schema);
				colvarPhonePrimary.ColumnName = "PhonePrimary";
				colvarPhonePrimary.DataType = DbType.String;
				colvarPhonePrimary.MaxLength = 50;
				colvarPhonePrimary.AutoIncrement = false;
				colvarPhonePrimary.IsNullable = true;
				colvarPhonePrimary.IsPrimaryKey = false;
				colvarPhonePrimary.IsForeignKey = false;
				colvarPhonePrimary.IsReadOnly = false;
				colvarPhonePrimary.DefaultSetting = @"";
				colvarPhonePrimary.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhonePrimary);
				
				TableSchema.TableColumn colvarPhoneSecondary = new TableSchema.TableColumn(schema);
				colvarPhoneSecondary.ColumnName = "PhoneSecondary";
				colvarPhoneSecondary.DataType = DbType.String;
				colvarPhoneSecondary.MaxLength = 50;
				colvarPhoneSecondary.AutoIncrement = false;
				colvarPhoneSecondary.IsNullable = true;
				colvarPhoneSecondary.IsPrimaryKey = false;
				colvarPhoneSecondary.IsForeignKey = false;
				colvarPhoneSecondary.IsReadOnly = false;
				colvarPhoneSecondary.DefaultSetting = @"";
				colvarPhoneSecondary.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneSecondary);
				
				TableSchema.TableColumn colvarPhoneMobile = new TableSchema.TableColumn(schema);
				colvarPhoneMobile.ColumnName = "PhoneMobile";
				colvarPhoneMobile.DataType = DbType.String;
				colvarPhoneMobile.MaxLength = 50;
				colvarPhoneMobile.AutoIncrement = false;
				colvarPhoneMobile.IsNullable = true;
				colvarPhoneMobile.IsPrimaryKey = false;
				colvarPhoneMobile.IsForeignKey = false;
				colvarPhoneMobile.IsReadOnly = false;
				colvarPhoneMobile.DefaultSetting = @"";
				colvarPhoneMobile.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoneMobile);
				
				TableSchema.TableColumn colvarPhoto = new TableSchema.TableColumn(schema);
				colvarPhoto.ColumnName = "Photo";
				colvarPhoto.DataType = DbType.String;
				colvarPhoto.MaxLength = 512;
				colvarPhoto.AutoIncrement = false;
				colvarPhoto.IsNullable = true;
				colvarPhoto.IsPrimaryKey = false;
				colvarPhoto.IsForeignKey = false;
				colvarPhoto.IsReadOnly = false;
				colvarPhoto.DefaultSetting = @"";
				colvarPhoto.ForeignKeyTableName = "";
				schema.Columns.Add(colvarPhoto);
				
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
				DataService.Providers["AceMail"].AddSchema("client",schema);
			}
		}
		#endregion
		
		#region Props
		  
		[XmlAttribute("Clientid")]
		[Bindable(true)]
		public string Clientid 
		{
			get { return GetColumnValue<string>(Columns.Clientid); }
			set { SetColumnValue(Columns.Clientid, value); }
		}
		  
		[XmlAttribute("FirstName")]
		[Bindable(true)]
		public string FirstName 
		{
			get { return GetColumnValue<string>(Columns.FirstName); }
			set { SetColumnValue(Columns.FirstName, value); }
		}
		  
		[XmlAttribute("MiddleName")]
		[Bindable(true)]
		public string MiddleName 
		{
			get { return GetColumnValue<string>(Columns.MiddleName); }
			set { SetColumnValue(Columns.MiddleName, value); }
		}
		  
		[XmlAttribute("LastName")]
		[Bindable(true)]
		public string LastName 
		{
			get { return GetColumnValue<string>(Columns.LastName); }
			set { SetColumnValue(Columns.LastName, value); }
		}
		  
		[XmlAttribute("Address1")]
		[Bindable(true)]
		public string Address1 
		{
			get { return GetColumnValue<string>(Columns.Address1); }
			set { SetColumnValue(Columns.Address1, value); }
		}
		  
		[XmlAttribute("Address2")]
		[Bindable(true)]
		public string Address2 
		{
			get { return GetColumnValue<string>(Columns.Address2); }
			set { SetColumnValue(Columns.Address2, value); }
		}
		  
		[XmlAttribute("City")]
		[Bindable(true)]
		public string City 
		{
			get { return GetColumnValue<string>(Columns.City); }
			set { SetColumnValue(Columns.City, value); }
		}
		  
		[XmlAttribute("StateCode")]
		[Bindable(true)]
		public string StateCode 
		{
			get { return GetColumnValue<string>(Columns.StateCode); }
			set { SetColumnValue(Columns.StateCode, value); }
		}
		  
		[XmlAttribute("PostalCode")]
		[Bindable(true)]
		public string PostalCode 
		{
			get { return GetColumnValue<string>(Columns.PostalCode); }
			set { SetColumnValue(Columns.PostalCode, value); }
		}
		  
		[XmlAttribute("CountryCode")]
		[Bindable(true)]
		public string CountryCode 
		{
			get { return GetColumnValue<string>(Columns.CountryCode); }
			set { SetColumnValue(Columns.CountryCode, value); }
		}
		  
		[XmlAttribute("PhonePrimary")]
		[Bindable(true)]
		public string PhonePrimary 
		{
			get { return GetColumnValue<string>(Columns.PhonePrimary); }
			set { SetColumnValue(Columns.PhonePrimary, value); }
		}
		  
		[XmlAttribute("PhoneSecondary")]
		[Bindable(true)]
		public string PhoneSecondary 
		{
			get { return GetColumnValue<string>(Columns.PhoneSecondary); }
			set { SetColumnValue(Columns.PhoneSecondary, value); }
		}
		  
		[XmlAttribute("PhoneMobile")]
		[Bindable(true)]
		public string PhoneMobile 
		{
			get { return GetColumnValue<string>(Columns.PhoneMobile); }
			set { SetColumnValue(Columns.PhoneMobile, value); }
		}
		  
		[XmlAttribute("Photo")]
		[Bindable(true)]
		public string Photo 
		{
			get { return GetColumnValue<string>(Columns.Photo); }
			set { SetColumnValue(Columns.Photo, value); }
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
		public static void Insert(string varClientid,string varFirstName,string varMiddleName,string varLastName,string varAddress1,string varAddress2,string varCity,string varStateCode,string varPostalCode,string varCountryCode,string varPhonePrimary,string varPhoneSecondary,string varPhoneMobile,string varPhoto,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Client item = new Client();
			
			item.Clientid = varClientid;
			
			item.FirstName = varFirstName;
			
			item.MiddleName = varMiddleName;
			
			item.LastName = varLastName;
			
			item.Address1 = varAddress1;
			
			item.Address2 = varAddress2;
			
			item.City = varCity;
			
			item.StateCode = varStateCode;
			
			item.PostalCode = varPostalCode;
			
			item.CountryCode = varCountryCode;
			
			item.PhonePrimary = varPhonePrimary;
			
			item.PhoneSecondary = varPhoneSecondary;
			
			item.PhoneMobile = varPhoneMobile;
			
			item.Photo = varPhoto;
			
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
		public static void Update(string varClientid,string varFirstName,string varMiddleName,string varLastName,string varAddress1,string varAddress2,string varCity,string varStateCode,string varPostalCode,string varCountryCode,string varPhonePrimary,string varPhoneSecondary,string varPhoneMobile,string varPhoto,DateTime varCreateddate,DateTime varUpdateddate)
		{
			Client item = new Client();
			
				item.Clientid = varClientid;
			
				item.FirstName = varFirstName;
			
				item.MiddleName = varMiddleName;
			
				item.LastName = varLastName;
			
				item.Address1 = varAddress1;
			
				item.Address2 = varAddress2;
			
				item.City = varCity;
			
				item.StateCode = varStateCode;
			
				item.PostalCode = varPostalCode;
			
				item.CountryCode = varCountryCode;
			
				item.PhonePrimary = varPhonePrimary;
			
				item.PhoneSecondary = varPhoneSecondary;
			
				item.PhoneMobile = varPhoneMobile;
			
				item.Photo = varPhoto;
			
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
        
        
        public static TableSchema.TableColumn ClientidColumn
        {
            get { return Schema.Columns[0]; }
        }
        
        
        
        public static TableSchema.TableColumn FirstNameColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn MiddleNameColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn LastNameColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        public static TableSchema.TableColumn Address1Column
        {
            get { return Schema.Columns[4]; }
        }
        
        
        
        public static TableSchema.TableColumn Address2Column
        {
            get { return Schema.Columns[5]; }
        }
        
        
        
        public static TableSchema.TableColumn CityColumn
        {
            get { return Schema.Columns[6]; }
        }
        
        
        
        public static TableSchema.TableColumn StateCodeColumn
        {
            get { return Schema.Columns[7]; }
        }
        
        
        
        public static TableSchema.TableColumn PostalCodeColumn
        {
            get { return Schema.Columns[8]; }
        }
        
        
        
        public static TableSchema.TableColumn CountryCodeColumn
        {
            get { return Schema.Columns[9]; }
        }
        
        
        
        public static TableSchema.TableColumn PhonePrimaryColumn
        {
            get { return Schema.Columns[10]; }
        }
        
        
        
        public static TableSchema.TableColumn PhoneSecondaryColumn
        {
            get { return Schema.Columns[11]; }
        }
        
        
        
        public static TableSchema.TableColumn PhoneMobileColumn
        {
            get { return Schema.Columns[12]; }
        }
        
        
        
        public static TableSchema.TableColumn PhotoColumn
        {
            get { return Schema.Columns[13]; }
        }
        
        
        
        public static TableSchema.TableColumn CreateddateColumn
        {
            get { return Schema.Columns[14]; }
        }
        
        
        
        public static TableSchema.TableColumn UpdateddateColumn
        {
            get { return Schema.Columns[15]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Clientid = @"clientid";
			 public static string FirstName = @"FirstName";
			 public static string MiddleName = @"MiddleName";
			 public static string LastName = @"LastName";
			 public static string Address1 = @"Address1";
			 public static string Address2 = @"Address2";
			 public static string City = @"City";
			 public static string StateCode = @"StateCode";
			 public static string PostalCode = @"PostalCode";
			 public static string CountryCode = @"CountryCode";
			 public static string PhonePrimary = @"PhonePrimary";
			 public static string PhoneSecondary = @"PhoneSecondary";
			 public static string PhoneMobile = @"PhoneMobile";
			 public static string Photo = @"Photo";
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
