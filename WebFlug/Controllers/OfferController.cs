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
        
        
        // GET: Offer/MakeOffer/5
        public ActionResult MakeOffer()
        {
            return View();
        }

        // POST: Offer/MakeOffer/5
        [HttpPost]
        public ActionResult MakeOffer(string TravellerReward,DateTime DeliverDate)
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
        

        // GET: Offer/Create
        public ActionResult Create()
        {
            //ViewBag.OrderId = new SelectList(db.orders, "Order_Id", "OrderNumber");
            return View();
        }

        // POST: Offer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TravellerReward,DeliverDate")] Offers offer)
        {
            //var order = new Orders();

            if (ModelState.IsValid)
            {


                offer.OfferSatatus = "Pending";

                DateTime today = DateTime.Today;
                today.ToString("dd-MM-yyyy");
                offer.CreationDate = today;

                db.offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.OrderId = new SelectList(db.orders, "Order_Id", "OrderNumber", offer.OrderId);
            return View(offer);
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
            //ViewBag.OrderId = new SelectList(db.orders, "Order_Id", "OrderNumber", offers.Order_Id);
            return View(offers);
        }

        // POST: Offer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Offer_Id,TravellerReward,CreationDate,OfferSatatus,DeliverDate,OrderId,Email")] Offers offers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderId = new SelectList(db.orders, "Order_Id", "OrderNumber", offers.Order_Id);
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
