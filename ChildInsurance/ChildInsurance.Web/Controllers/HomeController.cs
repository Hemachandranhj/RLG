using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChildInsurance.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var obj = new CareerOptionService.CareerOptionServiceClient();
            ViewData["ServiceData"] = obj.GetData(1);
            return View();
        }
    }
}