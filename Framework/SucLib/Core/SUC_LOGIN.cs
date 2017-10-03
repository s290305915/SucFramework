using System;
using System.Collections.Generic;
using System.Data;
using SucLib.Core;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
namespace SucLib.Model
{
    /// <summary>
    /// SUC_LOGIN:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [DataMap(TableName = "SUC_LOGIN")]
    public partial class SUC_LOGIN : DataBase<SUC_LOGIN>
    {
        public SUC_LOGIN()
        { }
        #region Model
        private int _id;
        private string _login_name;
        private string _password;
        private int? _is_locked;
        private string _locked_reason;
        private DateTime? _locked_date;
        private DateTime? _login_date;
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "ID")]
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "LOGIN_NAME")]
        public string LOGIN_NAME
        {
            set { _login_name = value; }
            get { return _login_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "PASSWORD")]
        public string PASSWORD
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "IS_LOCKED")]
        public int? IS_LOCKED
        {
            set { _is_locked = value; }
            get { return _is_locked; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "LOCKED_REASON")]
        public string LOCKED_REASON
        {
            set { _locked_reason = value; }
            get { return _locked_reason; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "LOCKED_DATE")]
        public DateTime? LOCKED_DATE
        {
            set { _locked_date = value; }
            get { return _locked_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "LOGIN_DATE")]
        public DateTime? LOGIN_DATE
        {
            set { _login_date = value; }
            get { return _login_date; }
        }
        #endregion Model
        IDBHelp db = DBFactory.Create(); //实例化工厂
        public IList<SUC_LOGIN> Find(string Sql)
        {
            Sql = string.IsNullOrEmpty(Sql) ? "" : " AND " + Sql;
            DataTable dt = db.GetDataTable("SELECT * FROM SUC_LOGIN WHERE 1=1 " + Sql);
            return EntityModel.ConvertTo<SUC_LOGIN>(dt);
        }
    }
}

