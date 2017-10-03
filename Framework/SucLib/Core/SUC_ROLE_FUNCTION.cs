using System;
using System.Collections.Generic;
using System.Data;
using SucLib.Core;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
namespace SucLib.Model
{
	/// <summary>
	/// SUC_ROLE_FUNCTION:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SUC_ROLE_FUNCTION
	{
		public SUC_ROLE_FUNCTION()
		{}
		#region Model
		private int? _role_id;
		private int? _function_id;
		/// <summary>
		/// 
		/// </summary>
		public int? ROLE_ID
		{
			set{ _role_id=value;}
			get{return _role_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? FUNCTION_ID
		{
			set{ _function_id=value;}
			get{return _function_id;}
		}
		#endregion Model
        IDBHelp db = DBFactory.Create(); //实例化工厂
        public IList<SUC_ROLE_FUNCTION> Find(string Sql)
        {
            Sql = string.IsNullOrEmpty(Sql) ? "" : " AND " + Sql;
            DataTable dt = db.GetDataTable("SELECT * FROM SUC_ROLE_FUNCTION WHERE 1=1 " + Sql);
            return EntityModel.ConvertTo<SUC_ROLE_FUNCTION>(dt);
        }
	}
}

