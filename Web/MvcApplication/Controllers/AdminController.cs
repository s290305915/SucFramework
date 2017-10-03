using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SucLib.Model;
using SucLib.Data.IDal;
using SucLib.Data.Factory;
using SucLib.Common;
using MvcApplication.Models;

namespace MvcApplication.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        IDBHelp db = DBFactory.Create();
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult OutLogin()
        {
            CacheUtil.Clear();
            return Content("Login");
        }

        public ActionResult _PartialContent()
        {
            return PartialView("_PartialContent");
        }

        public ActionResult _PartialUserList()
        {
            return PartialView("_PartialUserList");
        }


        public ActionResult _PartialModel()
        {
            return PartialView("_PartialModel");
        }

        public ActionResult _PartialRole()
        {
            return PartialView("_PartialRole");
        }

        [HttpPost]
        public ActionResult Login(Login l)
        {
            string code = "";
            string msg = "";
            if(string.IsNullOrEmpty(l.LOGIN_NAME) || string.IsNullOrEmpty(l.PASSWORD))
            {
                code = "7";
                msg = "请输入用户名密码！";
            }
            else
            {
                try
                {
                    SUC_LOGIN lg;
                    try
                    {
                        lg = new SUC_LOGIN().FindSingleByCondition(new SUC_LOGIN()
                        {
                            LOGIN_NAME = l.LOGIN_NAME,
                            PASSWORD = l.PASSWORD
                        });
                    }
                    catch
                    {
                        code = "4";
                        msg = "登陆失败，用户名密码不正确！";
                        return Json(new
                        {
                            code = code,
                            msg = msg
                        });
                    }
                    if(lg != null && lg.ID != 0)    //.LOGIN_NAME
                    {
                        SucCookie.Add("UserName", l);//.LOGIN_NAME
                        SucCookie.Add("UserID", lg.ID);
                        code = "1";
                        msg = "登陆成功！";
                    }
                    //code = "4";
                }
                catch(Exception ex)
                {
                    code = "7";
                    msg = $"出错了：{ex.Message}";
                }
            }
            return Json(new
            {
                code = code,
                msg = msg
            });
        }


        public ActionResult Index()
        {
            return View();
        }

    }
}
