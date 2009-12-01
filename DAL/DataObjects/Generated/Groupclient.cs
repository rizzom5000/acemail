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
	/// Strongly-typed collection for the Groupclient class.
	/// </summary>
    [Serializable]
	public partial class GroupclientCollection : ActiveList<Groupclient, GroupclientCollection>
	{	   
		public GroupclientCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>GroupclientCollection</returns>
		public GroupclientCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Groupclient o = this[i];
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
	/// This is an ActiveRecord class which wraps the groupclients table.
	/// </summary>
	[Serializable]
	public partial class Groupclient : ActiveRecord<Groupclient>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Groupclient()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Groupclient(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Groupclient(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Groupclient(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("groupclients", TableType.Table, DataService.GetInstance("AceMail"));
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
				
				TableSchema.TableColumn colvarClientemailid = new TableSchema.TableColumn(schema);
				colvarClientemailid.ColumnName = "clientemailid";
				colvarClientemailid.DataType = DbType.String;
				colvarClientemailid.MaxLength = 64;
				colvarClientemailid.AutoIncrement = false;
				colvarClientemailid.IsNullable = false;
				colvarClientemailid.IsPrimaryKey = false;
				colvarClientemailid.IsForeignKey = false;
				colvarClientemailid.IsReadOnly = false;
				colvarClientemailid.DefaultSetting = @"";
				colvarClientemailid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarClientemailid);
				
				TableSchema.TableColumn colvarClientsmsid = new TableSchema.TableColumn(schema);
				colvarClientsmsid.ColumnName = "clientsmsid";
				colvarClientsmsid.DataType = DbType.String;
				colvarClientsmsid.MaxLength = 64;
				colvarClientsmsid.AutoIncrement = false;
				colvarClientsmsid.IsNullable = true;
				colvarClientsmsid.IsPrimaryKey = false;
				colvarClientsmsid.IsForeignKey = false;
				colvarClientsmsid.IsReadOnly = false;
				colvarClientsmsid.DefaultSetting = @"";
				colvarClientsmsid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarClientsmsid);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["AceMail"].AddSchema("groupclients",schema);
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
		  
		[XmlAttribute("Clientid")]
		[Bindable(true)]
		public string Clientid 
		{
			get { return GetColumnValue<string>(Columns.Clientid); }
			set { SetColumnValue(Columns.Clientid, value); }
		}
		  
		[XmlAttribute("Clientemailid")]
		[Bindable(true)]
		public string Clientemailid 
		{
			get { return GetColumnValue<string>(Columns.Clientemailid); }
			set { SetColumnValue(Columns.Clientemailid, value); }
		}
		  
		[XmlAttribute("Clientsmsid")]
		[Bindable(true)]
		public string Clientsmsid 
		{
			get { return GetColumnValue<string>(Columns.Clientsmsid); }
			set { SetColumnValue(Columns.Clientsmsid, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varGroupid,string varClientid,string varClientemailid,string varClientsmsid)
		{
			Groupclient item = new Groupclient();
			
			item.Groupid = varGroupid;
			
			item.Clientid = varClientid;
			
			item.Clientemailid = varClientemailid;
			
			item.Clientsmsid = varClientsmsid;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varGroupid,string varClientid,string varClientemailid,string varClientsmsid)
		{
			Groupclient item = new Groupclient();
			
				item.Groupid = varGroupid;
			
				item.Clientid = varClientid;
			
				item.Clientemailid = varClientemailid;
			
				item.Clientsmsid = varClientsmsid;
			
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
        
        
        
        public static TableSchema.TableColumn ClientidColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        public static TableSchema.TableColumn ClientemailidColumn
        {
            get { return Schema.Columns[2]; }
        }
        
        
        
        public static TableSchema.TableColumn ClientsmsidColumn
        {
            get { return Schema.Columns[3]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Groupid = @"groupid";
			 public static string Clientid = @"clientid";
			 public static string Clientemailid = @"clientemailid";
			 public static string Clientsmsid = @"clientsmsid";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
