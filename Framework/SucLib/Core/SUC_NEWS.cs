using System;
using System.Collections.Generic;
using System.Data;
using SucLib.Core;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace SucLib.Model
{    /// <summary>
    /// SUC_NEWS :实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    [DataMap(TableName = "SUC_NEWS")]
    public partial class SUC_NEWS : DataBase<SUC_NEWS>
    {
        public SUC_NEWS()
        { }
        #region Model
        private int _id;
        private string _title;
        private string _author;
        private string _source;
        private DateTime _pubtime;
        private string _pubdate;
        private string _content;
        private string _pdurl;
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
        [DataMap(Column = "PUBTIME")]
        public DateTime PUBTIME
        {
            set { _pubtime = value; }
            get { return _pubtime; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "pandaWebUrl")]
        public string pandaWebUrl
        {
            set { _pdurl = value; }
            get { return _pdurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "PUBDATE")]
        public string PUBDATE
        {
            set { _pubdate = value; }
            get { return _pubdate; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "AUTHOR")]
        public string AUTHOR
        {
            set { _author = value; }
            get { return _author; }
        }
        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "SOURCE")]
        public string SOURCE
        {
            set { _source = value; }
            get { return _source; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "TITLE")]
        public string TITLE
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 
        /// </summary>
        [DataMap(Column = "CONTENT")]
        public string CONTENT
        {
            set { _content = value; }
            get { return _content; }
        }
        #endregion Model
    }
}
