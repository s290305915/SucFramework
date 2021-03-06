﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Linq
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
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SUCMSF")]
	public partial class DataClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertSUC_USER(SUC_USER instance);
    partial void UpdateSUC_USER(SUC_USER instance);
    partial void DeleteSUC_USER(SUC_USER instance);
    #endregion
		
		public DataClassesDataContext() : 
				base(global::Linq.Properties.Settings.Default.SUCMSFConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SUC_USER> SUC_USER
		{
			get
			{
				return this.GetTable<SUC_USER>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SUC_USER")]
	public partial class SUC_USER : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _NAME;
		
		private string _LOGIN_NAME;
		
		private System.Nullable<System.DateTime> _CREATE_TIME;
		
		private System.Nullable<int> _LOGIN_COUNT;
		
		private System.Nullable<System.DateTime> _LAST_LOGIN_TIME;
		
		private string _REMARK;
		
		private System.Nullable<int> _ROLE_ID;
		
		private string _PHONENO;
		
		private string _IP_ADDRESS;
		
		private string _UNIT;
		
		private System.Nullable<int> _IS_CHECKER;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNAMEChanging(string value);
    partial void OnNAMEChanged();
    partial void OnLOGIN_NAMEChanging(string value);
    partial void OnLOGIN_NAMEChanged();
    partial void OnCREATE_TIMEChanging(System.Nullable<System.DateTime> value);
    partial void OnCREATE_TIMEChanged();
    partial void OnLOGIN_COUNTChanging(System.Nullable<int> value);
    partial void OnLOGIN_COUNTChanged();
    partial void OnLAST_LOGIN_TIMEChanging(System.Nullable<System.DateTime> value);
    partial void OnLAST_LOGIN_TIMEChanged();
    partial void OnREMARKChanging(string value);
    partial void OnREMARKChanged();
    partial void OnROLE_IDChanging(System.Nullable<int> value);
    partial void OnROLE_IDChanged();
    partial void OnPHONENOChanging(string value);
    partial void OnPHONENOChanged();
    partial void OnIP_ADDRESSChanging(string value);
    partial void OnIP_ADDRESSChanged();
    partial void OnUNITChanging(string value);
    partial void OnUNITChanged();
    partial void OnIS_CHECKERChanging(System.Nullable<int> value);
    partial void OnIS_CHECKERChanged();
    #endregion
		
		public SUC_USER()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_NAME", DbType="VarChar(50)")]
		public string NAME
		{
			get
			{
				return this._NAME;
			}
			set
			{
				if ((this._NAME != value))
				{
					this.OnNAMEChanging(value);
					this.SendPropertyChanging();
					this._NAME = value;
					this.SendPropertyChanged("NAME");
					this.OnNAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LOGIN_NAME", DbType="VarChar(50)")]
		public string LOGIN_NAME
		{
			get
			{
				return this._LOGIN_NAME;
			}
			set
			{
				if ((this._LOGIN_NAME != value))
				{
					this.OnLOGIN_NAMEChanging(value);
					this.SendPropertyChanging();
					this._LOGIN_NAME = value;
					this.SendPropertyChanged("LOGIN_NAME");
					this.OnLOGIN_NAMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CREATE_TIME", DbType="DateTime")]
		public System.Nullable<System.DateTime> CREATE_TIME
		{
			get
			{
				return this._CREATE_TIME;
			}
			set
			{
				if ((this._CREATE_TIME != value))
				{
					this.OnCREATE_TIMEChanging(value);
					this.SendPropertyChanging();
					this._CREATE_TIME = value;
					this.SendPropertyChanged("CREATE_TIME");
					this.OnCREATE_TIMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LOGIN_COUNT", DbType="Int")]
		public System.Nullable<int> LOGIN_COUNT
		{
			get
			{
				return this._LOGIN_COUNT;
			}
			set
			{
				if ((this._LOGIN_COUNT != value))
				{
					this.OnLOGIN_COUNTChanging(value);
					this.SendPropertyChanging();
					this._LOGIN_COUNT = value;
					this.SendPropertyChanged("LOGIN_COUNT");
					this.OnLOGIN_COUNTChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LAST_LOGIN_TIME", DbType="DateTime")]
		public System.Nullable<System.DateTime> LAST_LOGIN_TIME
		{
			get
			{
				return this._LAST_LOGIN_TIME;
			}
			set
			{
				if ((this._LAST_LOGIN_TIME != value))
				{
					this.OnLAST_LOGIN_TIMEChanging(value);
					this.SendPropertyChanging();
					this._LAST_LOGIN_TIME = value;
					this.SendPropertyChanged("LAST_LOGIN_TIME");
					this.OnLAST_LOGIN_TIMEChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_REMARK", DbType="Text", UpdateCheck=UpdateCheck.Never)]
		public string REMARK
		{
			get
			{
				return this._REMARK;
			}
			set
			{
				if ((this._REMARK != value))
				{
					this.OnREMARKChanging(value);
					this.SendPropertyChanging();
					this._REMARK = value;
					this.SendPropertyChanged("REMARK");
					this.OnREMARKChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ROLE_ID", DbType="Int")]
		public System.Nullable<int> ROLE_ID
		{
			get
			{
				return this._ROLE_ID;
			}
			set
			{
				if ((this._ROLE_ID != value))
				{
					this.OnROLE_IDChanging(value);
					this.SendPropertyChanging();
					this._ROLE_ID = value;
					this.SendPropertyChanged("ROLE_ID");
					this.OnROLE_IDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PHONENO", DbType="VarChar(13)")]
		public string PHONENO
		{
			get
			{
				return this._PHONENO;
			}
			set
			{
				if ((this._PHONENO != value))
				{
					this.OnPHONENOChanging(value);
					this.SendPropertyChanging();
					this._PHONENO = value;
					this.SendPropertyChanged("PHONENO");
					this.OnPHONENOChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IP_ADDRESS", DbType="VarChar(100)")]
		public string IP_ADDRESS
		{
			get
			{
				return this._IP_ADDRESS;
			}
			set
			{
				if ((this._IP_ADDRESS != value))
				{
					this.OnIP_ADDRESSChanging(value);
					this.SendPropertyChanging();
					this._IP_ADDRESS = value;
					this.SendPropertyChanged("IP_ADDRESS");
					this.OnIP_ADDRESSChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_UNIT", DbType="VarChar(100)")]
		public string UNIT
		{
			get
			{
				return this._UNIT;
			}
			set
			{
				if ((this._UNIT != value))
				{
					this.OnUNITChanging(value);
					this.SendPropertyChanging();
					this._UNIT = value;
					this.SendPropertyChanged("UNIT");
					this.OnUNITChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IS_CHECKER", DbType="Int")]
		public System.Nullable<int> IS_CHECKER
		{
			get
			{
				return this._IS_CHECKER;
			}
			set
			{
				if ((this._IS_CHECKER != value))
				{
					this.OnIS_CHECKERChanging(value);
					this.SendPropertyChanging();
					this._IS_CHECKER = value;
					this.SendPropertyChanged("IS_CHECKER");
					this.OnIS_CHECKERChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
