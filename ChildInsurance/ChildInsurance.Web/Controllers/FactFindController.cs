using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ChildInsurance.Web.Models;
using System.IO;
using System.Xml;

namespace ChildInsurance.Web.Views
{
    public class FactFindController : Controller
    {
        [HttpGet]
        public ActionResult Index(string type)
        {
            SaveCareerType(type);

            FactFindViewModel model = new FactFindViewModel();

            ////id = (id == null) ? Guid.NewGuid() : id;

            var xmldoc = new XmlDataDocument();
            XmlNodeList xmlnode;
            XmlNode node;

            ////model.StudentId = id;
            string path = HttpContext.Server.MapPath("~/App_Data/FactFind.xml");
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmldoc.Load(fs);
            fs.Close();
            fs.Dispose();
            node = xmldoc.SelectSingleNode("StudentId");
            xmlnode = xmldoc.GetElementsByTagName("StudentId");
            Guid studentId = Guid.Parse(xmlnode[0].InnerText);
            ////if (id == studentId)
            ////{
                model.Asset = xmldoc.GetElementsByTagName("Asset")[0].InnerXml;
                model.Expenditure = xmldoc.GetElementsByTagName("Expenditure")[0].InnerXml;
                model.Income = xmldoc.GetElementsByTagName("Income")[0].InnerXml;
                model.Liablity = xmldoc.GetElementsByTagName("Liablity")[0].InnerXml;
            ////}

            ////model.StudentId = id;

            return View(model);
        }

        private void SaveCareerType(string type)
        {
            string file = Server.MapPath(@"~\App_Data\CareerType.xml");
            using (XmlWriter writer = XmlWriter.Create(file))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("Career");
                writer.WriteElementString("Type", type);
                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
        }

        [HttpPost]
        public ActionResult FactFind(FactFindViewModel model)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(FactFindViewModel));

            string path = HttpContext.Server.MapPath("~/App_Data/FactFind.xml");

            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, model);
            file.Close();
            return RedirectToAction("FactFind", "FactFind");
        }
    }
}