using System;
using System.Collections.Generic;
using System.Data;
using SucLib.Core;
using SucLib.Data.Factory;
using SucLib.Data.IDal;
namespace SucLib.Model
{
    /// <summary>
    /// SUC_FUNCTION:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SUC_FUNCTION
    {
        public SUC_FUNCTION()
        { }
        #region Model
        private int _id;
        private string _name;
        private string _code;
        private string _description;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NAME
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CODE
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DESCRIPTION
        {
            set { _description = value; }
            get { return _description; }
        }
        #endregion Model
        IDBHelp db = DBFactory.Create(); //实例化工厂
        public IList<SUC_FUNCTION> Find(string Sql)
        {
            Sql = string.IsNullOrEmpty(Sql) ? "" : " AND " + Sql;
            DataTable dt = db.GetDataTable("SELECT * FROM SUC_FUNCTION WHERE 1=1 " + Sql);
            return EntityModel.ConvertTo<SUC_FUNCTION>(dt);
        }
    }
}

