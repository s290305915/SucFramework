using System;
using System.Collections.Generic;
using System.Data;
using SucLib.Core;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
namespace SucLib.Model
{
    /// <summary>
    /// SUC_ROLE:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [DataMap(TableName = "SUC_ROLE")]
    public partial class SUC_ROLE : DataBase<SUC_ROLE>
    {
        public SUC_ROLE()
        {
        }
        #region Model
        private int _id;
        private string _name;
        private string _remark;
        private int? _type;
        private int? _level;
        private List<SUC_FUNCTION> _funcs;
        private List<SUC_MODULE> _models;
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
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "TYPE")]
        public int? TYPE
        {
            set
            {
                _type = value;
            }
            get
            {
                return _type;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "LEVEL")]
        public int? LEVEL
        {
            set
            {
                _level = value;
            }
            get
            {
                return _level;
            }
        }

        /// <summary>
        /// 多对多的关系，role->function
        /// </summary>
        [DataMap(HasMany = true, ManyTableName = "SUC_ROLE_FUNCTION", ManyCloumnSelf = "ROLE_ID", ManyClounmOther = "FUNCTION_ID")]
        public List<SUC_FUNCTION> FUNCTIONS
        {
            set
            {
                _funcs = value;
            }
            get
            {
                return _funcs;
            }
        }

        /// <summary>
        /// 多对多的关系，role->module
        /// </summary>
        [DataMap(HasMany = true, ManyTableName = "SUC_ROLE_MODULE", ManyCloumnSelf = "ROLE_ID", ManyClounmOther = "MODULE_ID")]
        public List<SUC_MODULE> MODULES
        {
            set
            {
                _models = value;
            }
            get
            {
                return _models;
            }
        }

        #endregion Model
        //IDBHelp db = DBFactory.Create(); //实例化工厂

        //public IList<SUC_ROLE> Find(string Sql, params object[] args)
        //{
        //    return Find(string.Format(Sql, args));
        //}
        //public IList<SUC_ROLE> Find(string Sql)
        //{
        //    Sql = string.IsNullOrEmpty(Sql) ? "" : " AND " + Sql;
        //    DataTable dt = db.GetDataTable("SELECT * FROM SUC_ROLE WHERE 1=1 " + Sql);
        //    return EntityModel.ConvertTo<SUC_ROLE>(dt);
        //}
    }
}

