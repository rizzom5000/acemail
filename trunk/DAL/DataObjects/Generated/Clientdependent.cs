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
	/// Strongly-typed collection for the Clientdependent class.
	/// </summary>
    [Serializable]
	public partial class ClientdependentCollection : ActiveList<Clientdependent, ClientdependentCollection>
	{	   
		public ClientdependentCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>ClientdependentCollection</returns>
		public ClientdependentCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Clientdependent o = this[i];
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
	/// This is an ActiveRecord class which wraps the clientdependents table.
	/// </summary>
	[Serializable]
	public partial class Clientdependent : ActiveRecord<Clientdependent>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Clientdependent()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Clientdependent(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Clientdependent(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Clientdependent(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("clientdependents", TableType.Table, DataService.GetInstance("AceMail"));
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
				
				TableSchema.TableColumn colvarDependentid = new TableSchema.TableColumn(schema);
				colvarDependentid.ColumnName = "dependentid";
				colvarDependentid.DataType = DbType.String;
				colvarDependentid.MaxLength = 64;
				colvarDependentid.AutoIncrement = false;
				colvarDependentid.IsNullable = false;
				colvarDependentid.IsPrimaryKey = true;
				colvarDependentid.IsForeignKey = false;
				colvarDependentid.IsReadOnly = false;
				colvarDependentid.DefaultSetting = @"";
				colvarDependentid.ForeignKeyTableName = "";
				schema.Columns.Add(colvarDependentid);
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["AceMail"].AddSchema("clientdependents",schema);
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
		  
		[XmlAttribute("Dependentid")]
		[Bindable(true)]
		public string Dependentid 
		{
			get { return GetColumnValue<string>(Columns.Dependentid); }
			set { SetColumnValue(Columns.Dependentid, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varClientid,string varDependentid)
		{
			Clientdependent item = new Clientdependent();
			
			item.Clientid = varClientid;
			
			item.Dependentid = varDependentid;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varClientid,string varDependentid)
		{
			Clientdependent item = new Clientdependent();
			
				item.Clientid = varClientid;
			
				item.Dependentid = varDependentid;
			
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
        
        
        
        public static TableSchema.TableColumn DependentidColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Clientid = @"clientid";
			 public static string Dependentid = @"dependentid";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
