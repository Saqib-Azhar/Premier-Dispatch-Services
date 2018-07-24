using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PracticingSMTP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PaperWorkPlan(FormCollection fc, HttpPostedFileBase file)
        {
            var Carrier = fc["Carrier"];
            var Trailer = fc["Trailer"];
            var AreasOfOperation = fc["AreasOfOperation"];
            var LiabilityInCargoInsurance = fc["LiabilityInCargoInsurance"];
            var FreightInvoices = fc["FreightInvoices"];
            var NoOfTrucksInFleet = fc["NoOfTrucksInFleet"];
            var SafetyRating = fc["SafetyRating"];
            var NoOfCompanyTrucks = fc["NoOfCompanyTrucks"];
            var MinRatePerMile = fc["MinRatePerMile"];
            var MaxPickAmount = fc["MaxPickAmount"];
            var MaxDropsAmount = fc["MaxDropsAmount"];
            var HazmatCertified = fc["HazmatCertified"];
            var ScacCode = fc["ScacCode"];
            var TwicCard = fc["TwicCard"];
            var TruckNo = fc["TruckNo"];
            var TrailerNo = fc["TrailerNo"];
            var TrailerType = fc["TrailerType"];
            var MaxWeight = fc["MaxWeight"];
            var FirstName = fc["FirstName"];
            var LastName = fc["LastName"];
            var Company = fc["Company"];
            var MCNo = fc["MCNo"];
            var FAX = fc["FAX"];
            var YearsInBusiness = fc["YearsInBusiness"];
            var EMAIL = fc["EMAIL"];
            var PHONE = fc["PHONE"];
            var File = fc["File"];
            return View();
        }
    }
}