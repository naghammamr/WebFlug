using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebFlug.Models;

namespace WebFlug.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        //show users
        [Authorize(Roles = "Admin")]
        public ActionResult showUsers()
        {
            ViewBag.Message = "Show all users page.";
            return View(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var Customer = db.Users.Find(id);
            if (Customer == null)
            {
                return HttpNotFound();
            }
            return View(Customer);
        }

        [HttpPost]
        public ActionResult Delete(ApplicationUser user)
        {
            var Customer = db.Users.Find(user.Id);
            db.Users.Remove(Customer);
            db.SaveChanges();

            return RedirectToAction("showUsers");
        }

        /*FAQ */
        //GET : Admin/ViewFAQ
        public ActionResult ViewFAQ()
        {
            return View(db.FAQs.ToList());
        }

        //GET : Admin/ADDFAQ
        public ActionResult AddFAQ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFAQ(FAQ faq)
        {
            db.FAQs.Add(faq);
            db.SaveChanges();

            return RedirectToAction("ViewFAQ");
        }

        public ActionResult EditFAQ(int id)
        {
            return View(db.FAQs.Where(c => c.FAQ_ID.Equals(id)).SingleOrDefault());
        }

        [HttpPost]
        public ActionResult EditFAQ(FAQ faq)
        {
            db.Entry<FAQ>(faq).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ViewFAQ");
        }

        public ActionResult DeleteFAQ(int id)
        {
            var faqq = db.FAQs.Where(u => u.FAQ_ID.Equals(id)).SingleOrDefault();
            db.FAQs.Remove(faqq);
            db.SaveChanges();
            return RedirectToAction("ViewFAQ");
        }
        /*endd FAQ*/
        [HttpGet]
        public ActionResult Block(string id)
        {
            var Customer = db.Users.Find(id);
            if (Customer == null)
            {
                return HttpNotFound();
            }
            return View(Customer);

        }

        [HttpPost]
        public ActionResult Block(ApplicationUser user)
        {
            var Customer = db.Users.Find(user.Id);
            Customer.IsBlocked = true;
            db.SaveChanges();

            return RedirectToAction("showUsers");
        }



        [HttpGet]
        public ActionResult UnBlock(string id)
        {
            var Customer = db.Users.Find(id);
            if (Customer == null)
            {
                return HttpNotFound();
            }
            return View(Customer);

        }

        [HttpPost]
        public ActionResult UnBlock(ApplicationUser user)
        {
            var Customer = db.Users.Find(user.Id);
            Customer.IsBlocked = false;
            db.SaveChanges();

            return RedirectToAction("showUsers");
        }

    }


}