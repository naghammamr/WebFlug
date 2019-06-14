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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var offers = db.offers.Include(o => o.orders);
            return View(offers.ToList());
        }

        public ActionResult OfferAccepted()
        {
            var UserId = User.Identity.GetUserId();
            var offers = db.offers.Where(a => a.UserId == UserId && a.OfferSatatus == "Accepted").ToList();
            return View(offers);
        }

        public ActionResult ConfirmedOffers()
        {
            var UserId = User.Identity.GetUserId();
            var offers = db.offers.Where(a => a.UserId == UserId && a.OfferSatatus == "Confirmed").ToList();
            return View(offers);
        }

        public ActionResult DoneOffers()
        {
            var UserId = User.Identity.GetUserId();
            var offers = db.offers.Where(a => a.UserId == UserId && a.OfferSatatus == "Done").ToList();
            return View(offers);
        }
        
        // GET: MyOffers
        [Authorize(Roles = "User")]
        public ActionResult MyOffers()
        {
            var UserId = User.Identity.GetUserId();
            var offers = db.offers.Where(a => a.UserId == UserId && a.OfferSatatus == "Pending").ToList();
            return View(offers);
        }

        // GET: Offer/MakeOffer/5
        [Authorize(Roles = "User")]
        public ActionResult MakeOffer()
        {
            return View();
        }

        // POST: Offer/MakeOffer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
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

        // GET: Offer/Edit/5
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        public ActionResult Edit(Offers offers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(offers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyOffers");
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
            return RedirectToAction("MyOffers");
        }

        //
        public Offers RetriveOfferByID(int ID)
        {
            var model = db.offers.FirstOrDefault(x => x.Offer_Id == ID);
            return model;
        }

        //
        public Orders RetriveOrderByID(int ID)
        {
            var model = db.orders.FirstOrDefault(x => x.Order_Id == ID);
            return model;
        }

        // GET: Order/ConfirmBuy/5
        public ActionResult ConfirmBuy()
        {
            return View();
        }

        // POST: Order/ConfirmBuy/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmBuy(int id)
        {
            var Offermodel = RetriveOfferByID(id);
            Offermodel.OfferSatatus = "Confirmed";
            UpdateModel<Offers>(Offermodel);
            db.SaveChanges();

            var OrderModel = RetriveOrderByID(Offermodel.Order_Id);
            OrderModel.OrderSatatus = "Confirmed";
            UpdateModel<Orders>(OrderModel);
            db.SaveChanges();

            return RedirectToAction("MyOffers");
        }

        //
        public ActionResult DoneDelivering()
        {
            return View();
        }

        // POST: Order/ConfirmBuy/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoneDelivering(int id)
        {
            var Offermodel = RetriveOfferByID(id);
            Offermodel.OfferSatatus = "Done";
            UpdateModel<Offers>(Offermodel);
            db.SaveChanges();
            
            return RedirectToAction("SendEmail","EmailSetup");
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