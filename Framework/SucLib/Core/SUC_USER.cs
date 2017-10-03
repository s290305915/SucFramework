using System;
using System.Collections.Generic;
using System.Data;
using SucLib.Core;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
namespace SucLib.Model
{
    /// <summary>
    /// SUC_USER:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [DataMap(TableName = "SUC_USER")]
    public partial class SUC_USER : DataBase<SUC_USER>
    {
        public SUC_USER()
        {
        }
        #region Model
        private int _id;
        private string _name;
        private string _login_name;
        private DateTime? _create_time;
        private int? _login_count;
        private DateTime? _last_login_time;
        private string _remark;
        private int? _role_id;  //
        private SUC_ROLE _roles;
        private SUC_LOGIN _login;
        private string _phoneno;
        private string _ip_address;
        private string _unit;
        private int? _is_checker;
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "ID")]
        public int ID
        {
            set
            {
                _id = value;
            }
            get
            {
                return _id;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "NAME")]
        public string NAME
        {
            set
            {
                _name = value;
            }
            get
            {
                return _name;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "LOGIN_NAME")]
        public string LOGIN_NAME
        {
            set
            {
                _login_name = value;
            }
            get
            {
                return _login_name;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "CREATE_TIME")]
        public DateTime? CREATE_TIME
        {
            set
            {
                _create_time = value;
            }
            get
            {
                return _create_time;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "LOGIN_COUNT")]
        public int? LOGIN_COUNT
        {
            set
            {
                _login_count = value;
            }
            get
            {
                return _login_count;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "LAST_LOGIN_TIME")]
        public DateTime? LAST_LOGIN_TIME
        {
            set
            {
                _last_login_time = value;
            }
            get
            {
                return _last_login_time;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "REMARK")]
        public string REMARK
        {
            set
            {
                _remark = value;
            }
            get
            {
                return _remark;
            }
        }
        ///// <summary>
        ///// 
        ///// </summary>
        [DataMap(Column = "ROLE_ID")]
        public int? ROLE_ID
        {
            set
            {
                _role_id = value;
            }
            get
            {
                return _role_id;
            }
        }

        [DataMap(BelongsTo = "SUC_ROLE", BelongsToKey = "ROLE_ID", TargetKey = "ID")]
        public SUC_ROLE ROLE
        {
            set
            {
                _roles = value;
            }
            get
            {
                return _roles;
            }
        }

        [DataMap(BelongsTo = "SUC_LOGIN", BelongsToKey = "LOGIN_NAME", TargetKey = "LOGIN_NAME")]
        public SUC_LOGIN LOGIN
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "PHONENO")]
        public string PHONENO
        {
            set
            {
                _phoneno = value;
            }
            get
            {
                return _phoneno;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "IP_ADDRESS")]
        public string IP_ADDRESS
        {
            set
            {
                _ip_address = value;
            }
            get
            {
                return _ip_address;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "UNIT")]
        public string UNIT
        {
            set
            {
                _unit = value;
            }
            get
            {
                return _unit;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "IS_CHECKER")]
        public int? IS_CHECKER
        {
            set
            {
                _is_checker = value;
            }
            get
            {
                return _is_checker;
            }
        }

        public string Rolename
        {
            get; set;
        }
        #endregion Model
        //IDBHelp db = DBFactory.Create(); //实例化工厂
        //public IList<SUC_USER> Find(string Sql)
        //{
        //    Sql = string.IsNullOrEmpty(Sql) ? "" : " AND " + Sql;
        //    DataTable dt = db.GetDataTable("SELECT * FROM SUC_USER WHERE 1=1 " + Sql);
        //    return EntityModel.ConvertTo<SUC_USER>(dt);
        //}
    }
}

