using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChildInsurance.Web.Models;

namespace ChildInsurance.Web.Controllers
{
    public class IllustrationController : Controller
    {
        public ActionResult Index()
        {
            string xmlData = HttpContext.Server.MapPath("~/App_Data/Illustration.xml");//Path of the xml script  
            DataSet ds = new DataSet();//Using dataset to read xml file  
            ds.ReadXml(xmlData);
            IllustrationViewModel illustration = (from rows in ds.Tables[0].AsEnumerable()
                                         select new IllustrationViewModel
                                         {
                                             Age = Convert.ToInt32(rows[0]),
                                             Term = Convert.ToInt32(rows[1]),
                                             Mode = Convert.ToString(rows[2]),
                                             Premium = Convert.ToInt32(rows[3]),
                                             SumAssured = Convert.ToInt32(rows[4])
                                         }).FirstOrDefault();

            return View(illustration);
        }
    }
}