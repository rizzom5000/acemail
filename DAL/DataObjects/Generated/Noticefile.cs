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
	/// Strongly-typed collection for the Noticefile class.
	/// </summary>
    [Serializable]
	public partial class NoticefileCollection : ActiveList<Noticefile, NoticefileCollection>
	{	   
		public NoticefileCollection() {}
        
        /// <summary>
		/// Filters an existing collection based on the set criteria. This is an in-memory filter
		/// Thanks to developingchris for this!
        /// </summary>
        /// <returns>NoticefileCollection</returns>
		public NoticefileCollection Filter()
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                Noticefile o = this[i];
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
	/// This is an ActiveRecord class which wraps the noticefiles table.
	/// </summary>
	[Serializable]
	public partial class Noticefile : ActiveRecord<Noticefile>, IActiveRecord
	{
		#region .ctors and Default Settings
		
		public Noticefile()
		{
		  SetSQLProps();
		  InitSetDefaults();
		  MarkNew();
		}
		
		private void InitSetDefaults() { SetDefaults(); }
		
		public Noticefile(bool useDatabaseDefaults)
		{
			SetSQLProps();
			if(useDatabaseDefaults)
				ForceDefaults();
			MarkNew();
		}
        
		public Noticefile(object keyID)
		{
			SetSQLProps();
			InitSetDefaults();
			LoadByKey(keyID);
		}
		 
		public Noticefile(string columnName, object columnValue)
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
				TableSchema.Table schema = new TableSchema.Table("noticefiles", TableType.Table, DataService.GetInstance("AceMail"));
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
				
				BaseSchema = schema;
				//add this schema to the provider
				//so we can query it later
				DataService.Providers["AceMail"].AddSchema("noticefiles",schema);
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
		  
		[XmlAttribute("Fileid")]
		[Bindable(true)]
		public string Fileid 
		{
			get { return GetColumnValue<string>(Columns.Fileid); }
			set { SetColumnValue(Columns.Fileid, value); }
		}
		
		#endregion
		
		
			
		
		//no foreign key tables defined (0)
		
		
		
		//no ManyToMany tables defined (0)
		
        
        
		#region ObjectDataSource support
		
		
		/// <summary>
		/// Inserts a record, can be used with the Object Data Source
		/// </summary>
		public static void Insert(string varNoticeid,string varFileid)
		{
			Noticefile item = new Noticefile();
			
			item.Noticeid = varNoticeid;
			
			item.Fileid = varFileid;
			
		
			if (System.Web.HttpContext.Current != null)
				item.Save(System.Web.HttpContext.Current.User.Identity.Name);
			else
				item.Save(System.Threading.Thread.CurrentPrincipal.Identity.Name);
		}
		
		/// <summary>
		/// Updates a record, can be used with the Object Data Source
		/// </summary>
		public static void Update(string varNoticeid,string varFileid)
		{
			Noticefile item = new Noticefile();
			
				item.Noticeid = varNoticeid;
			
				item.Fileid = varFileid;
			
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
        
        
        
        public static TableSchema.TableColumn FileidColumn
        {
            get { return Schema.Columns[1]; }
        }
        
        
        
        #endregion
		#region Columns Struct
		public struct Columns
		{
			 public static string Noticeid = @"noticeid";
			 public static string Fileid = @"fileid";
						
		}
		#endregion
		
		#region Update PK Collections
		
        #endregion
    
        #region Deep Save
		
        #endregion
	}
}
