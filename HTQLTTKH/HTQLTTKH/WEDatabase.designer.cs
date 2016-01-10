﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HTQLTTKH
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="WiseEyeV5Express")]
	public partial class WEDatabaseDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public WEDatabaseDataContext() : 
				base(global::HTQLTTKH.Properties.Settings.Default.WiseEyeV5ExpressConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public WEDatabaseDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WEDatabaseDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WEDatabaseDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public WEDatabaseDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<UserAccount> UserAccounts
		{
			get
			{
				return this.GetTable<UserAccount>();
			}
		}
		
		public System.Data.Linq.Table<NewUserAccount> NewUserAccounts
		{
			get
			{
				return this.GetTable<NewUserAccount>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.NewUserAccount_DocTatCaTaiKhoanV6")]
		public ISingleResult<NewUserAccount_DocTatCaTaiKhoanV6Result> NewUserAccount_DocTatCaTaiKhoanV6([global::System.Data.Linq.Mapping.ParameterAttribute(Name="Enable", DbType="Bit")] System.Nullable<bool> enable, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="UserAccount", DbType="NVarChar(50)")] string userAccount, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="EncryptPassword", DbType="NVarChar(100)")] string encryptPassword)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), enable, userAccount, encryptPassword);
			return ((ISingleResult<NewUserAccount_DocTatCaTaiKhoanV6Result>)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.UserAccount")]
	public partial class UserAccount
	{
		
		private int _UserID;
		
		private string _UserAccount1;
		
		private string _Pass;
		
		private System.Nullable<int> _Privilege;
		
		private System.Nullable<int> _AreaID;
		
		public UserAccount()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="UserAccount", Storage="_UserAccount1", DbType="NVarChar(50)")]
		public string UserAccount1
		{
			get
			{
				return this._UserAccount1;
			}
			set
			{
				if ((this._UserAccount1 != value))
				{
					this._UserAccount1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Pass", DbType="NVarChar(50)")]
		public string Pass
		{
			get
			{
				return this._Pass;
			}
			set
			{
				if ((this._Pass != value))
				{
					this._Pass = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Privilege", DbType="Int")]
		public System.Nullable<int> Privilege
		{
			get
			{
				return this._Privilege;
			}
			set
			{
				if ((this._Privilege != value))
				{
					this._Privilege = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_AreaID", DbType="Int")]
		public System.Nullable<int> AreaID
		{
			get
			{
				return this._AreaID;
			}
			set
			{
				if ((this._AreaID != value))
				{
					this._AreaID = value;
				}
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.NewUserAccount")]
	public partial class NewUserAccount
	{
		
		private int _UserID;
		
		private string _UserAccount;
		
		private string _Password;
		
		private bool _Enable;
		
		public NewUserAccount()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", AutoSync=AutoSync.Always, DbType="Int NOT NULL IDENTITY", IsDbGenerated=true)]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserAccount", DbType="NVarChar(50)")]
		public string UserAccount
		{
			get
			{
				return this._UserAccount;
			}
			set
			{
				if ((this._UserAccount != value))
				{
					this._UserAccount = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Password", DbType="NVarChar(100)")]
		public string Password
		{
			get
			{
				return this._Password;
			}
			set
			{
				if ((this._Password != value))
				{
					this._Password = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Enable", DbType="Bit NOT NULL")]
		public bool Enable
		{
			get
			{
				return this._Enable;
			}
			set
			{
				if ((this._Enable != value))
				{
					this._Enable = value;
				}
			}
		}
	}
	
	public partial class NewUserAccount_DocTatCaTaiKhoanV6Result
	{
		
		private string _UserAccount;
		
		private int _UserID;
		
		private bool _Enable;
		
		public NewUserAccount_DocTatCaTaiKhoanV6Result()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserAccount", DbType="NVarChar(50)")]
		public string UserAccount
		{
			get
			{
				return this._UserAccount;
			}
			set
			{
				if ((this._UserAccount != value))
				{
					this._UserAccount = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UserID", DbType="Int NOT NULL")]
		public int UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				if ((this._UserID != value))
				{
					this._UserID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Enable", DbType="Bit NOT NULL")]
		public bool Enable
		{
			get
			{
				return this._Enable;
			}
			set
			{
				if ((this._Enable != value))
				{
					this._Enable = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
