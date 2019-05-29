using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using WebFlug.Models;

namespace WebFlug.Controllers
{
    public class PaymentController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }
    }
}