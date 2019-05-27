using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebFlug.Models;

namespace WebFlug.Controllers
{
    public class OfferController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Offer
        public ActionResult Index()
        {
            var offers = db.offers.Include(o => o.orders);
            return View(offers.ToList());
        }

        // GET: Offer/MakeOffer/5
        public ActionResult MakeOffer()
        {
            return View();
        }

        // POST: Offer/MakeOffer/5
        [HttpPost]
        public ActionResult MakeOffer(string TravellerReward, DateTime DeliverDate)
        {
            var userID = User.Identity.GetUserId();
            var orderID = (int)Session["Order_Id"];

            var offer = new Offers();

            if (ModelState.IsValid)
            {
                offer.Order_Id = orderID;

                offer.UserId = userID;

                offer.OfferSatatus = "Pending";

                DateTime today = DateTime.Today;
                today.ToString("dd-MM-yyyy");
                offer.CreationDate = today;

                offer.TravellerReward = TravellerReward;
                offer.DeliverDate = DeliverDate;

                db.offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

    
        // GET: Offer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offers offers = db.offers.Find(id);
            if (offers == null)
            {
                return HttpNotFound();
            }
            return View(offers);
        }
        

        // GET: Offer/Create
        public ActionResult Create()
        {
            //ViewBag.OrderId = new SelectList(db.orders, "Order_Id", "OrderNumber");
            return View();
        }

     

        // GET: Offer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offers offers = db.offers.Find(id);
            if (offers == null)
            {
                return HttpNotFound();
            }
           
            return View(offers);
        }

        // POST: Offer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Offers offers)
        {
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                //offers.UserId = userID;

                Offers obj = new Offers { Offer_Id = offers.Offer_Id };
                db.offers.Attach(obj);

                obj.Order_Id = offers.Order_Id;
                obj.CreationDate = offers.CreationDate;
                obj.OfferSatatus = offers.OfferSatatus;

                db.Entry(offers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(offers);
        }

        // GET: Offer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Offers offers = db.offers.Find(id);
            if (offers == null)
            {
                return HttpNotFound();
            }
            return View(offers);
        }

        // POST: Offer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Offers offers = db.offers.Find(id);
            db.offers.Remove(offers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AcceptOffer(int? id)
        {
            Orders accept = db.orders.Find(id);
            accept.OrderSatatus = "InProgress";

            return Redirect("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
