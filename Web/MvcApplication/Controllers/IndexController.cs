using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using SucLib.Model;
using SucLib.Common;
using MvcApplication.App_Data;
using System.Web.Script.Serialization;
using System.Text;

namespace MvcApplication.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/
        IDBHelp db = DBFactory.Create();
        public ActionResult Index()
        {
            return View();
        }

        public string SaveUser(int? id, string login_name, string name, string phone, string role, string chker, string utn)
        {
            #region MyRegion

            try
            {
                if(id == null)
                {
                    SUC_USER u = new SUC_USER()
                    {
                        CREATE_TIME = DateTime.Now,
                        IP_ADDRESS = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString(),
                        IS_CHECKER = Convert.ToInt32(chker),
                        LAST_LOGIN_TIME = DateTime.Now,
                        LOGIN_COUNT = 0,
                        LOGIN = new SUC_LOGIN()
                        {
                            IS_LOCKED = 0,
                            LOGIN_NAME = login_name,
                            PASSWORD = "1"
                        },
                        LOGIN_NAME = login_name,
                        NAME = name,
                        PHONENO = phone,
                        REMARK = "",
                        ROLE = new SUC_ROLE().FindByCondition(new SUC_ROLE() { NAME = role })[0],
                        ROLE_ID = new SUC_ROLE().FindByCondition(new SUC_ROLE() { NAME = role })[0].ID,
                        UNIT = utn
                    };
                    return u.Add(u) == true ? "success" : "添加失败";
                }
                else
                {
                    SUC_USER u = new SUC_USER()
                    {
                        ID = (int)id,
                        IP_ADDRESS = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString(),
                        IS_CHECKER = Convert.ToInt32(chker),
                        LOGIN_NAME = login_name,
                        NAME = name,
                        PHONENO = phone,
                        REMARK = "",
                        ROLE = new SUC_ROLE().FindByCondition(new SUC_ROLE() { NAME = role })[0],
                        ROLE_ID = new SUC_ROLE().FindByCondition(new SUC_ROLE() { NAME = role })[0].ID,
                        UNIT = utn,
                        CREATE_TIME = null,
                        LAST_LOGIN_TIME = null,
                        LOGIN_COUNT = null
                    };
                    return u.Update(u) == true ? "success" : "修改失败";
                    #endregion

                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetRoleList()
        {
            List<SUC_ROLE> rs = new SUC_ROLE().FindAll();
            JSS.Serialize(rs, sb);
            return sb.ToString();
        }

        public string GetUserList(string s_name)
        {
            List<SUC_USER> us = new List<SUC_USER>();
            SUC_USER u = new SUC_USER();
            if(!string.IsNullOrEmpty(s_name))
            {
                u = new SUC_USER() { NAME = s_name };
            }
            us = u.FindByCondition(u);
            if(us != null)
                us.ForEach(x => x.Rolename = x.ROLE.NAME);
            JSS.Serialize(us, sb);
            return sb.ToString();
        }

        StringBuilder sb = new StringBuilder();
        JavaScriptSerializer JSS = new JavaScriptSerializer();
        public string GetMenuData()
        {
            //string json = "[{\"UserId\": \"1\",\"MenuId\": \"1\",\"ParentId\": \"0\",\"FullName\": \"权限应用\",\"Description\": \"\",\"Img\": \"eye.png\",\"NavigateUrl\": \"\",\"FormName\": \"\",\"Target\": \"Click\",\"IsUnfold\": \"1\"},";
            //json+="{\"UserId\": \"1\",\"MenuId\": \"2\",\"ParentId\": \"1\",\"FullName\": \"职员管理\",\"Description\": \"\",\"Img\": \"people.png\",\"NavigateUrl\": \"/CommonModule/Employee/EmployeeIndex.html\",\"FormName\": \"\",\"Target\": \"Iframe\",\"IsUnfold\": \"0\"}";
            //json+="]";

            List<TreeMenu> ltm = new List<TreeMenu>();
            List<SUC_MODULE> ms = new List<SUC_MODULE>();
            ms = new SUC_MODULE().FindAll();
            foreach(SUC_MODULE m in ms)
            {
                TreeMenu t = new TreeMenu()
                {
                    Description = "",//m.DESCRPTION,
                    FormName = m.NAME,
                    FullName = m.NAME,
                    Img = m.IMG,
                    IsUnfold = m.PARENT_ID == 0 ? 1 : 0,
                    MenuId = m.ID,
                    NavigateUrl = m.LOCATION,
                    ParentId = m.PARENT_ID,
                    Target = m.PARENT_ID == 0 ? "Click" : "Iframe",
                    UserId = AppHelper.GetCurrentUser().ID
                };
                ltm.Add(t);
            }
            JSS.Serialize(ltm, sb);
            return sb.ToString();
        }

    }

    public class TreeMenu
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId
        {
            get; set;
        }
        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId
        {
            get; set;
        }
        /// <summary>
        /// 父id
        /// </summary>
        public int? ParentId
        {
            get; set;
        }
        /// <summary>
        /// 展示名
        /// </summary>
        public string FullName
        {
            get; set;
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            get; set;
        }
        /// <summary>
        /// 图标
        /// </summary>
        public string Img
        {
            get; set;
        }
        /// <summary>
        /// 链接（如果为文件夹则为空）
        /// </summary>
        public string NavigateUrl
        {
            get; set;
        }
        /// <summary>
        /// 打开新窗口展示在窗口上的title
        /// </summary>
        public string FormName
        {
            get; set;
        }
        /// <summary>
        /// 点击方式,文件夹Click,具体项目Iframe
        /// </summary>
        public string Target
        {
            get; set;
        }
        /// <summary>
        /// 是否为文件夹
        /// </summary>
        public int IsUnfold
        {
            get; set;
        }
    }
}
