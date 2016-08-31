using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChildInsurance.Model.Service;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using ChildInsurance.Web.Models;
using ChildInsurance.Model.CareerRecommedation;

namespace ChildInsurance.Web.Controllers
{
    public class CareerOptionController : Controller
    {
        private CareerOptionService.CareerOptionServiceClient serviceClient = new CareerOptionService.CareerOptionServiceClient();

        private CareerOptionViewModel viewModel = new CareerOptionViewModel();

        public ActionResult CareerOption()
        {
            ProcessAcademicCareerOption();

            ProcessNonAcademicCareerOption();

            return View(viewModel);
        }

        private void ProcessAcademicCareerOption()
        {
            viewModel.AcademicCareerRecommedation = new List<CareerRecommedation>();

            string filePath = Server.MapPath(@"~\App_Data\AcademicInterest.xml");
            var interests = GetInterests(filePath);

            var request = new InterestRequest
            {
                InterestName = interests
            };

            WriteCsvFile(request, "Chartered Accountant", "AcademicData_Eval");

            var careerOption = serviceClient.GetAcademicCareerOptions();

            PopulateAcademicCareerOptionViewModel(interests, careerOption);
        }

        private void ProcessNonAcademicCareerOption()
        {
            viewModel.NonAcademicCareerRecommedation = new List<CareerRecommedation>();

            string filePath = Server.MapPath(@"~\App_Data\NonAcademicInterest.xml");
            var interests = GetInterests(filePath);

            var request = new InterestRequest
            {
                InterestName = interests
            };

            WriteCsvFile(request, "VFX Designer", "InterestData_Eval");

            var careerOption = serviceClient.GetNonAcademyCareerOption();

            PopulateNonAcademicCareerOptionViewModel(interests, careerOption);
        }

        private void PopulateAcademicCareerOptionViewModel(List<string> interests, string careerOption)
        {
            viewModel.AcademicCareerRecommedation = new List<CareerRecommedation>();

            var interestModel = ConvertRequestToModel(interests);

            var careerRecommedation = new CareerRecommedation();
            careerRecommedation.Interests = interestModel;
            careerRecommedation.CareerOption = careerOption;

            viewModel.AcademicCareerRecommedation.Add(careerRecommedation);
        }

        private void PopulateNonAcademicCareerOptionViewModel(List<string> interests, string careerOption)
        {
            viewModel.NonAcademicCareerRecommedation = new List<CareerRecommedation>();

            var interestModel = ConvertRequestToModel(interests);

            var careerRecommedation = new CareerRecommedation();
            careerRecommedation.Interests = interestModel;
            careerRecommedation.CareerOption = careerOption;

            viewModel.NonAcademicCareerRecommedation.Add(careerRecommedation);
        }

        private IList<Interest> ConvertRequestToModel(IList<string> interests)
        {
            var interestModel = new List<Interest>();

            interests.ToList().ForEach(e => 
                        {
                            interestModel.Add(new Interest
                            {
                                InterestName = e
                            });
                        });

            return interestModel;
        }

        private void WriteCsvFile(InterestRequest interestRequest, string tempCareerOption, string fileName)
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
                csvContent += e.Trim() + ",";
            });

            csvContent += tempCareerOption;
            sb.AppendLine(csvContent);

            string file = Server.MapPath(@"~\App_Data\" + fileName + ".csv");
            System.IO.File.WriteAllText(file, sb.ToString());
        }

        private List<string> GetInterests(string file)
        {
            var interests = new List<string>();
            
            XDocument interestXml = XDocument.Load(file);
            var interestElements = interestXml.Elements().Elements("InterestName");

            interestElements.ToList().ForEach(e =>
            {
                interests.Add(e.Value);
            });

            return interests;
        }
    }
}