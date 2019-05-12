using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebFlug.Models;

namespace WebFlug.Controllers
{
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order
        public IEnumerable<Orders> GetOrders()
        {
            var orders = db.orders.ToList();
            return orders;
        }

        public ActionResult Index()
        {
            var orders = GetOrders();
            return View(orders);
        }


        public static string GetUniqueKey(int size)
        {
            char[] chars = "ABLNOPSTUWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }



        //show old orders
        public ActionResult ViewOldOrders()
        {
            var user = User.Identity.GetUserId();
            var currentEmail = db.Users.Find(user);
            var myorders = db.orders.Where(e => e.Email == currentEmail.Email);
            return View(myorders);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        //Bind-- to protect against over-posting in create scenarios
        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cross-site request forgery attacks
        public ActionResult Create(Orders order, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                order.ProductImage = upload.FileName;

                var userID = User.Identity.GetUserId();
                order.UserId = userID;

                string number = GetUniqueKey(6);
                order.OrderNumber = number;

                order.Email = User.Identity.Name;

                order.OrderSatatus = "Requested";

                DateTime today = DateTime.Today;
                today.ToString("dd-MM-yyyy");
                order.CreationDate = today;

                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }


        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = GetOrders().SingleOrDefault(o => o.Order_Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            Session["Order_Id"] = id;
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Order_Id,OrderNumber,ProductName,ProductDetails,ProductPrice,ProductQuantity,ProductImage,ProductLink,ProductWeight,Deliverfrom,DeliverToDeliverDate,AdditionalDetails,OrderSatatus,CreationDate,Email")] Orders order, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    order.ProductImage = upload.FileName;
                }
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders order = db.orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders order = db.orders.Find(id);
            db.orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
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