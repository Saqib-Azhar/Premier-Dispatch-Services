using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace PracticingSMTP.Controllers
{
    public class HomeController : Controller
    {
        private static string SenderEmailId = WebConfigurationManager.AppSettings["DefaultEmailId"];
        private static string SenderEmailPassword = WebConfigurationManager.AppSettings["DefaultEmailPassword"];
        private static int SenderEmailPort = Convert.ToInt32(WebConfigurationManager.AppSettings["DefaultEmailPort"]);
        private static string SenderEmailHost = WebConfigurationManager.AppSettings["DefaultEmailHost"]; 
        private static string ReceiverEmailId = WebConfigurationManager.AppSettings["ReceiverEmailId"]; 

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

        public ActionResult SubmitForm(FormCollection oCollection)
        {
            try
            {
                string htmlString = "<b><u>Premier Dispatch LLC</u></b>";
                foreach (var key in oCollection.Keys)
                {
                    if (key.ToString() == "signatureValue")
                    {
                        continue;
                    }
                    htmlString = htmlString + "<b>" + key.ToString().ToUpper() + ":</b>" + oCollection[key.ToString()] + "<br>";
                }

                var fromAddress = new MailAddress(SenderEmailId, "Premier Dispatch LLC");
                var toAddress = new MailAddress(ReceiverEmailId, "Premier Dispatch LLC");
                string fromPassword = SenderEmailPassword;
                string subject = "Premier Dispatch LLC form Submitted";
                string body = htmlString + "<br><b>Form Submitted At: " + DateTime.Now + "</b>";

                var smtp = new SmtpClient
                {
                    Host = SenderEmailHost,
                    Port = SenderEmailPort,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };

                var graphClass = new GraphicsController();
                var res = graphClass.GetBase64Png(oCollection["signatureValue"], 25, 25);
                byte[] bytes = Convert.FromBase64String(res);

                Image image;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                stream.Position = 0;
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body,

                })
                {
                    message.Attachments.Add(new Attachment(stream, "image/png"));
                    smtp.Send(message);
                }


            }
            catch (Exception ex)
            {

                ExceptionHandlerController.infoMessage(ex.Message);
                ExceptionHandlerController.writeErrorLog(ex);

            }
            var urlReferrer = Request.UrlReferrer;
            return Redirect(urlReferrer.AbsoluteUri);
        }
    }
}