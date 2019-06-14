using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFlug.Servicese;

namespace notification29.Controllers
{
    public class NotificationController : Controller
    {
        //
        // GET: /Notification/
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetNotification()
        {
            return Json(SendNotifications.GetNotification(), JsonRequestBehavior.AllowGet);

        }
	}
}