using ChildInsurance.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;

namespace ChildInsurance.Web.Controllers
{
    public class FinancialRecommendationController : Controller
    {
        private FinancialRecommendationService.FinancialRecommendationServiceClient financialService = new FinancialRecommendationService.FinancialRecommendationServiceClient();

        public ActionResult Index()
        {
            var model = new FactFindViewModel();

            var xmldoc = new XmlDataDocument();

            ViewData["Selected"] = "Domestic";

            XmlNodeList xmlnode;
            XmlNodeList xmlcareer;

            string path = HttpContext.Server.MapPath("~/App_Data/FactFind.xml");

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();

            xmlnode = xmldoc.GetElementsByTagName("StudentId");
            
            model.Asset = xmldoc.GetElementsByTagName("Asset")[0].InnerXml;
            model.Expenditure = xmldoc.GetElementsByTagName("Expenditure")[0].InnerXml;
            model.Income = xmldoc.GetElementsByTagName("Income")[0].InnerXml;
            model.Liablity = xmldoc.GetElementsByTagName("Liablity")[0].InnerXml;

            path = HttpContext.Server.MapPath("~/App_Data/Career.xml");
            xmldoc = new XmlDataDocument();
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();

            xmlcareer = xmldoc.GetElementsByTagName("career");

            var academic = xmldoc.GetElementsByTagName("academic")[0].InnerXml;

            var nonAcademic = xmldoc.GetElementsByTagName("nonacademic")[0].InnerXml;

            academic = academic.Replace(" ", "");
            nonAcademic = nonAcademic.Replace(" ", "");

            path = HttpContext.Server.MapPath("~/App_Data/Domestic.xml");
            xmldoc = new XmlDataDocument();
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();

            var currentAcademicCost = xmldoc.GetElementsByTagName(academic)[0].InnerXml;
            var currentNonAcademicCost = xmldoc.GetElementsByTagName(nonAcademic)[0].InnerXml;

            path = HttpContext.Server.MapPath("~/App_Data/Careertype.xml");
            xmldoc = new XmlDataDocument();
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();

            xmlcareer = xmldoc.GetElementsByTagName("Career");

            var academictype = xmldoc.GetElementsByTagName("Type")[0].InnerXml;

            var costtocalculate = (academictype == "Academic" ? currentAcademicCost : currentNonAcademicCost);

            var AspirationCost = financialService.FutureCost(Convert.ToDecimal(costtocalculate), 8, 7);

            var assestCost = financialService.FutureCost(Convert.ToDecimal(model.Asset), 8, 7);
            var financialModel = new FinancialViewModel();

            financialModel.ActualCost = assestCost;

            financialModel.ProjectedCost = AspirationCost;

            financialModel.Years = 7;
            financialModel.ShortFall = AspirationCost - assestCost;

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(FinancialViewModel));

            path = HttpContext.Server.MapPath("~/App_Data/FinancialRecommendation.xml");

            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, financialModel);
            file.Close();

            return View();
        }

        public ActionResult AbroadIndex()
        {
            var model = new FactFindViewModel();

            var xmldoc = new XmlDataDocument();
            ViewData["Selected"] = "Abroad";
            XmlNodeList xmlnode;
            XmlNodeList xmlcareer;

            string path = HttpContext.Server.MapPath("~/App_Data/FactFind.xml");

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();

            xmlnode = xmldoc.GetElementsByTagName("StudentId");

            model.Asset = xmldoc.GetElementsByTagName("Asset")[0].InnerXml;
            model.Expenditure = xmldoc.GetElementsByTagName("Expenditure")[0].InnerXml;
            model.Income = xmldoc.GetElementsByTagName("Income")[0].InnerXml;
            model.Liablity = xmldoc.GetElementsByTagName("Liablity")[0].InnerXml;

            path = HttpContext.Server.MapPath("~/App_Data/Career.xml");
            xmldoc = new XmlDataDocument();
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();

            xmlcareer = xmldoc.GetElementsByTagName("career");

            var academic = xmldoc.GetElementsByTagName("academic")[0].InnerXml;

            var nonAcademic = xmldoc.GetElementsByTagName("nonacademic")[0].InnerXml;

            academic = academic.Replace(" ", "");
            nonAcademic = nonAcademic.Replace(" ", "");

            path = HttpContext.Server.MapPath("~/App_Data/abroad.xml");
            xmldoc = new XmlDataDocument();
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();

            var currentAcademicCost = xmldoc.GetElementsByTagName(academic)[0].InnerXml;
            var currentNonAcademicCost = xmldoc.GetElementsByTagName(nonAcademic)[0].InnerXml;

            path = HttpContext.Server.MapPath("~/App_Data/Careertype.xml");
            xmldoc = new XmlDataDocument();
            fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();

            xmlcareer = xmldoc.GetElementsByTagName("Career");

            var academictype = xmldoc.GetElementsByTagName("Type")[0].InnerXml;
            
            var costtocalculate = (academictype == "academic" ? currentAcademicCost : currentNonAcademicCost);

            var AspirationCost = financialService.FutureCost(Convert.ToDecimal(costtocalculate), 8, 7);

            var assestCost = financialService.FutureCost(Convert.ToDecimal(model.Asset), 8, 7);

            var financialModel = new FinancialViewModel();

            financialModel.ActualCost = assestCost;

            financialModel.ProjectedCost = AspirationCost;

            financialModel.Years = 7;
            financialModel.ShortFall = AspirationCost - assestCost;

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(FinancialViewModel));

            path = HttpContext.Server.MapPath("~/App_Data/FinancialRecommendation.xml");

            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, financialModel);
            file.Close();

            return View();
        }


        [HttpGet]
        public ActionResult FactFindData()
        {
            var xmldoc = new XmlDataDocument();
            string path = HttpContext.Server.MapPath("~/App_Data/FactFind.xml");
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();

            var Asset = xmldoc.GetElementsByTagName("Asset")[0].InnerXml;
            var Expenditure = xmldoc.GetElementsByTagName("Expenditure")[0].InnerXml;
            var Income = xmldoc.GetElementsByTagName("Income")[0].InnerXml;
            var Liablity = xmldoc.GetElementsByTagName("Liablity")[0].InnerXml;
            var StudentId = Guid.Parse(xmldoc.GetElementsByTagName("StudentId")[0].InnerXml);

            var FactFind = new List<object>();

            FactFind.Add(new { y = Asset, legendText = "Asset", indexLabel = "Asset" });
            FactFind.Add(new { y = Expenditure, legendText = "Expenditure", indexLabel = "Expenditure" });
            FactFind.Add(new { y = Income, legendText = "Income", indexLabel = "Income" });
            FactFind.Add(new { y = Liablity, legendText = "Liablity", indexLabel = "Liablity" });

            JavaScriptSerializer jss = new JavaScriptSerializer();

            string output = jss.Serialize(FactFind);
            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetFinancialRecomandation()
        {
            string xmlData = HttpContext.Server.MapPath("~/App_Data/FinancialRecommendation.xml");//Path of the xml script  
            DataSet ds = new DataSet();//Using dataset to read xml file  
            ds.ReadXml(xmlData);
            FinancialRecomandation financialRecomandation = (from rows in ds.Tables[0].AsEnumerable()
                                                             select new FinancialRecomandation
                                                             {
                                                                 ActualCost = Convert.ToInt32(rows[0]),
                                                                 YearDifference = Convert.ToInt32(rows[1]),
                                                                 ProjectedCost = Convert.ToInt32(rows[2]),
                                                                 ShortFallCost = Convert.ToInt32(rows[3])
                                                             }).FirstOrDefault();
            return Json(financialRecomandation, JsonRequestBehavior.AllowGet);
        }
    }
}