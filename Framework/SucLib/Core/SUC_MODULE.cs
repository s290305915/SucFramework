using System;
using System.Collections.Generic;
using System.Data;
using SucLib.Core;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
namespace SucLib.Model
{
    /// <summary>
    /// SUC_MODULE:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [DataMap(TableName = "SUC_MODULE")]
    public partial class SUC_MODULE : DataBase<SUC_MODULE>
    {
        public SUC_MODULE()
        {
        }
        #region Model
        private int _id;
        private int? _parent_id;
        private string _name;
        private string _location;
        private string _code;
        private int? _is_hide;
        private int? _show_order;
        private string _descrption;
        private string _img;
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
        [DataMap(Column = "PARENT_ID")]
        public int? PARENT_ID
        {
            set
            {
                _parent_id = value;
            }
            get
            {
                return _parent_id;
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
        [DataMap(Column = "LOCATION")]
        public string LOCATION
        {
            set
            {
                _location = value;
            }
            get
            {
                return _location;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "CODE")]
        public string CODE
        {
            set
            {
                _code = value;
            }
            get
            {
                return _code;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "IS_HIDE")]
        public int? IS_HIDE
        {
            set
            {
                _is_hide = value;
            }
            get
            {
                return _is_hide;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "SHOW_ORDER")]
        public int? SHOW_ORDER
        {
            set
            {
                _show_order = value;
            }
            get
            {
                return _show_order;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "DESCRPTION")]
        public string DESCRPTION
        {
            set
            {
                _descrption = value;
            }
            get
            {
                return _descrption;
            }
        }
        [DataMap(Column = "IMG")]
        public string IMG
        {
            set
            {
                _img = value;
            }
            get
            {
                return _img;
            }
        }
        #endregion Model

        //IDBHelp db = DBFactory.Create(); //实例化工厂
        //public IList<SUC_MODULE> Find(string Sql)
        //{
        //    Sql = string.IsNullOrEmpty(Sql) ? "" : " AND " + Sql;
        //    DataTable dt = db.GetDataTable("SELECT * FROM SUC_MODULE WHERE 1=1 " + Sql);
        //    return EntityModel.ConvertTo<SUC_MODULE>(dt);
        //}
    }
}

