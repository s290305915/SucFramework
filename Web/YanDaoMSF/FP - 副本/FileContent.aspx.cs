using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using SucLib.Common;
using System.Data;

namespace YanDaoMSF.FP
{
    public partial class FileContent : System.Web.UI.Page
    {
        public string Name;
        public int BrowNum;
        public int DownloadNum;
        public string PublishDate;
        public string FilePath;
        public string id;
        IDBHelp db = DBFactory.Create();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                id = Request.QueryString["id"];
                GetInfo(id);
            }
        }

        public void GetInfo(string id)
        {
            DataTable dt = db.GetDataTable(string.Format(@"SELECT * FROM SUC_FILES WHERE ID={0}", id));
            Name = dt.Rows[0]["NAME"].ToString();
            BrowNum = Convert.ToInt32(dt.Rows[0]["BROWNUM"].ToString());
            DownloadNum = Convert.ToInt32(dt.Rows[0]["DOWNLOADNUM"].ToString());
            PublishDate = Convert.ToDateTime(dt.Rows[0]["PUBLISH_DATE"].ToString()).ToShortDateString();
            FilePath = dt.Rows[0]["FILEPATH"].ToString();
        }
    }
}