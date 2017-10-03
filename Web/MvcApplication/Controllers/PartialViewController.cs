using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication.Controllers
{
    public class PartialViewController : Controller
    {
        //PartialView
        // GET: /PartialView/

        public ActionResult Index()
        {
            return View();
        }

    }
}
