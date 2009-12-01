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
	#region Tables Struct
	public partial struct Tables
	{
		
		public static string Client = @"client";
        
		public static string Clientdependent = @"clientdependents";
        
		public static string Email = @"email";
        
		public static string Eml = @"eml";
        
		public static string File = @"file";
        
		public static string Group = @"group";
        
		public static string Groupclient = @"groupclients";
        
		public static string Notice = @"notice";
        
		public static string Noticeclient = @"noticeclients";
        
		public static string Noticeeml = @"noticeemls";
        
		public static string Noticefile = @"noticefiles";
        
		public static string Noticegroup = @"noticegroups";
        
		public static string Referral = @"referral";
        
		public static string User = @"user";
        
		public static string Userclient = @"userclients";
        
		public static string Usercredential = @"usercredentials";
        
		public static string Usereml = @"useremls";
        
		public static string Userfile = @"userfiles";
        
		public static string Usergroup = @"usergroups";
        
		public static string Usernotice = @"usernotices";
        
	}
	#endregion
    #region Schemas
    public partial class Schemas {
		
		public static TableSchema.Table Client{
            get { return DataService.GetSchema("client","AceMail"); }
		}
        
		public static TableSchema.Table Clientdependent{
            get { return DataService.GetSchema("clientdependents","AceMail"); }
		}
        
		public static TableSchema.Table Email{
            get { return DataService.GetSchema("email","AceMail"); }
		}
        
		public static TableSchema.Table Eml{
            get { return DataService.GetSchema("eml","AceMail"); }
		}
        
		public static TableSchema.Table File{
            get { return DataService.GetSchema("file","AceMail"); }
		}
        
		public static TableSchema.Table Group{
            get { return DataService.GetSchema("group","AceMail"); }
		}
        
		public static TableSchema.Table Groupclient{
            get { return DataService.GetSchema("groupclients","AceMail"); }
		}
        
		public static TableSchema.Table Notice{
            get { return DataService.GetSchema("notice","AceMail"); }
		}
        
		public static TableSchema.Table Noticeclient{
            get { return DataService.GetSchema("noticeclients","AceMail"); }
		}
        
		public static TableSchema.Table Noticeeml{
            get { return DataService.GetSchema("noticeemls","AceMail"); }
		}
        
		public static TableSchema.Table Noticefile{
            get { return DataService.GetSchema("noticefiles","AceMail"); }
		}
        
		public static TableSchema.Table Noticegroup{
            get { return DataService.GetSchema("noticegroups","AceMail"); }
		}
        
		public static TableSchema.Table Referral{
            get { return DataService.GetSchema("referral","AceMail"); }
		}
        
		public static TableSchema.Table User{
            get { return DataService.GetSchema("user","AceMail"); }
		}
        
		public static TableSchema.Table Userclient{
            get { return DataService.GetSchema("userclients","AceMail"); }
		}
        
		public static TableSchema.Table Usercredential{
            get { return DataService.GetSchema("usercredentials","AceMail"); }
		}
        
		public static TableSchema.Table Usereml{
            get { return DataService.GetSchema("useremls","AceMail"); }
		}
        
		public static TableSchema.Table Userfile{
            get { return DataService.GetSchema("userfiles","AceMail"); }
		}
        
		public static TableSchema.Table Usergroup{
            get { return DataService.GetSchema("usergroups","AceMail"); }
		}
        
		public static TableSchema.Table Usernotice{
            get { return DataService.GetSchema("usernotices","AceMail"); }
		}
        
	
    }
    #endregion
    #region View Struct
    public partial struct Views 
    {
		
    }
    #endregion
    
    #region Query Factories
	public static partial class DB
	{
        public static DataProvider _provider = DataService.Providers["AceMail"];
        static ISubSonicRepository _repository;
        public static ISubSonicRepository Repository {
            get {
                if (_repository == null)
                    return new SubSonicRepository(_provider);
                return _repository; 
            }
            set { _repository = value; }
        }
	
        public static Select SelectAllColumnsFrom<T>() where T : RecordBase<T>, new()
	    {
            return Repository.SelectAllColumnsFrom<T>();
            
	    }
	    public static Select Select()
	    {
            return Repository.Select();
	    }
	    
		public static Select Select(params string[] columns)
		{
            return Repository.Select(columns);
        }
	    
		public static Select Select(params Aggregate[] aggregates)
		{
            return Repository.Select(aggregates);
        }
   
	    public static Update Update<T>() where T : RecordBase<T>, new()
	    {
            return Repository.Update<T>();
	    }
     
	    
	    public static Insert Insert()
	    {
            return Repository.Insert();
	    }
	    
	    public static Delete Delete()
	    {
            
            return Repository.Delete();
	    }
	    
	    public static InlineQuery Query()
	    {
            
            return Repository.Query();
	    }
	    	    
	    
	}
    #endregion
    
}
#region Databases
public partial struct Databases 
{
	
	public static string AceMail = @"AceMail";
    
}
#endregion