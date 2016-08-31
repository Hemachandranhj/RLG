using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChildInsurance.Model.Service;
using System.Xml;
using System.Xml.Linq;

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

            var serviceClient = new CareerOptionService.CareerOptionServiceClient();

            serviceClient.GetNonAcademyCareerOption(request);

            return View();
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