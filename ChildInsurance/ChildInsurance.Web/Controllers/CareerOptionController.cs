using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChildInsurance.Model.Service;
using System.Xml;
using System.Xml.Linq;
using System.Text;

namespace ChildInsurance.Web.Controllers
{
    public class CareerOptionController : Controller
    {
        private CareerOptionService.CareerOptionServiceClient serviceClient = new CareerOptionService.CareerOptionServiceClient();

        public ActionResult Index()
        {
            var interests = GetInterests();

            var request = new InterestRequest
            {
                InterestName = interests
            };

            WriteCsvFile(request);

            var careerOption = serviceClient.GetNonAcademyCareerOption();

            return View(careerOption);
        }

        private void WriteCsvFile(InterestRequest interestRequest)
        {
            StringBuilder sb = new StringBuilder();

            var csvContent = string.Empty;

            for (int i = 1; i <= interestRequest.InterestName.Count; i++)
            {
                csvContent += "Interest" + i + ",";
            }

            csvContent += "Career";
            sb.AppendLine(csvContent);

            csvContent = string.Empty;

            interestRequest.InterestName.ToList().ForEach(e =>
            {
                csvContent += e + ",";
            });

            csvContent += "VFX Designer";
            sb.AppendLine(csvContent);

            string file = Server.MapPath(@"~\App_Data\InterestData_Eval.csv");
            System.IO.File.WriteAllText(file, sb.ToString());
        }

        private List<string> GetInterests()
        {
            var interests = new List<string>();

            string filePath = Server.MapPath(@"~\App_Data\Interests.xml");
            XDocument interestXml = XDocument.Load(filePath);
            var interestElements = interestXml.Elements().Elements("InterestName");

            interestElements.ToList().ForEach(e =>
            {
                interests.Add(e.Value);
            });

            return interests;
        }
    }
}