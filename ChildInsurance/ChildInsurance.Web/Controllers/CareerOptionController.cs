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
        // GET: CareerOption
        public ActionResult Index()
        {
            var interests = GetInterests();
            
            var request = new InterestRequest
            {
                InterestName = interests
            };

            WriteCsvFile(request);

            var serviceClient = new CareerOptionService.CareerOptionServiceClient();

            serviceClient.GetNonAcademyCareerOption(request);

            return View();
        }

        private void WriteCsvFile(InterestRequest interestRequest)
        {
            var csvContent = new StringBuilder();

            var header = string.Empty;

            for (int i = 1; i <= interestRequest.InterestName.Count; i++)
            {
                //Add the Header row for CSV file.
                header += "Interest" + i;

                if (i != interestRequest.InterestName.Count)
                {
                    header += ",";
                }
            }

            csvContent.AppendLine(header);

            var content = string.Empty;

            interestRequest.InterestName.ToList().ForEach(e =>
            {
                content += e + ",";
            });

            csvContent.AppendLine(content);

            string path = HttpContext.Server.MapPath("~/App_Data/InterestData.csv");
            System.IO.FileStream file = System.IO.File.Create(path);

            System.IO.File.AppendAllText(path, csvContent.ToString());
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