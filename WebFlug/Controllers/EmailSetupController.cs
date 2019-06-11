using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using WebFlug.Models;

namespace WebFlug.Controllers
{
    public class EmailSetupController : Controller
    {
        // GET: Feedback
        [Authorize(Roles = "User")]
        public ActionResult SendEmail()
        {
            return View();
        }

        // Feedback
        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult SendEmail(Email model)
        {
            MailMessage mm = new MailMessage("flugapplication@gmail.com", "naghamamr@gmail.com");
            mm.Subject = "Feedback / Question";
            mm.Body = model.Body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("flugapplication@gmail.com", "@El7el7el7");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "your Feedback / Question has been sent succefully";

            return RedirectToAction("Index","Home");
        }

        // GET: 
        [Authorize(Roles = "User")]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult ContactUs(Email model)
        {
            MailMessage mm = new MailMessage("flugapplication@gmail.com", "naghamamr@gmail.com");
            mm.Subject = "Feedback / Question";
            mm.Body = model.Body;
            mm.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            NetworkCredential nc = new NetworkCredential("flugapplication@gmail.com", "@El7el7el7");
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "your Feedback / Question has been sent succefully";

            return RedirectToAction("Index", "Home");
        }
    }
}