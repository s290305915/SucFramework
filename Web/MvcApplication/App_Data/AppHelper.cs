using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SucLib.Model;
using SucLib.Common;
using SucLib.Data.Factory;
using SucLib.Data.IDal;

namespace MvcApplication.App_Data
{
    public static class AppHelper
    {
        static IDBHelp db = DBFactory.Create();
        public static SUC_USER GetCurrentUser()
        {
            int id = Convert.ToInt32(SucCookie.Read("UserID"));
            SUC_USER u = new SUC_USER().FindByCondition(new SUC_USER() { ID = id })[0];
            return u;
        }
    }
}