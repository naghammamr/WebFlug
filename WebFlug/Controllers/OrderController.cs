using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebFlug.Models;
using WebFlug.ViewModels;

namespace WebFlug.Controllers
{
    public class OrderController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        //Admin
        // GET: Order Requested
        public IEnumerable<Orders> GetRequestedOrders()
        {
            var orders = db.orders.ToList();
            return orders;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var orders = GetRequestedOrders();
            return View(orders);
        }
        
        public ActionResult IndexInProgress()
        {
            var UserId = User.Identity.GetUserId();
            var orders = db.orders.Where(x => x.OrderSatatus == "InProgress" && x.UserId == UserId).ToList();
            return View(orders);
        }
        
        public ActionResult IndexConfirmed()
        {
            var UserId = User.Identity.GetUserId();
            var orders = db.orders.Where(x => x.OrderSatatus == "Confirmed" && x.UserId == UserId).ToList();
            return View(orders);
        }

        public ActionResult IndexDelivered()
        {
            var UserId = User.Identity.GetUserId();
            var orders = db.orders.Where(x => x.OrderSatatus == "Recieved" && x.UserId == UserId).ToList();
            return View(orders);
        }

        public static string GetUniqueKey(int size)
        {
            char[] chars = "ABKHLNOPSTUWXYZ1234567890".ToCharArray();
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

        //show My orders
        [Authorize(Roles = "User")]
        public ActionResult ViewOldOrders()
        {
            var UserId = User.Identity.GetUserId();
            var orders = db.orders.Where(x => x.OrderSatatus == "Requested" && x.UserId == UserId).ToList();
            return View(orders);
        }

        // GET: Order/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult Create(Orders order, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                order.UserId = userID;

                string number = GetUniqueKey(6);
                order.OrderNumber = number;

                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                order.ProductImage = upload.FileName;

                order.CreationDate = DateTime.Now;

                order.OrderSatatus = "Requested";

                db.orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("ViewOldOrders", "Order");
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
            var order = GetRequestedOrders().SingleOrDefault(o => o.Order_Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            Session["Order_Id"] = id;
            return View(order);
        }

        // GET: Order/Details/5
        [AllowAnonymous]
        public ActionResult ToMakeOffer_OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = GetRequestedOrders().SingleOrDefault(o => o.Order_Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            Session["Order_Id"] = id;
            return View(order);
        }

        // GET: Order/Edit/5
        [Authorize(Roles = "User")]
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
        [Authorize(Roles = "User")]
        public ActionResult Edit(Orders order, int id, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    order.ProductImage = upload.FileName;
                }
                order.Order_Id = id;
                db.Entry(order).State = EntityState.Modified;
                if(db.SaveChanges()>0)
                {
                  return RedirectToAction("ViewOldOrders");
                }
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
            return RedirectToAction("ViewOldOrders");
        }

        //GET :viewmodel
        [Authorize(Roles = "User")]
        public ActionResult offersOForder(int? id)
        {
            var UserId = User.Identity.GetUserId();
            var Orders = db.orders.Where(a => a.UserId == UserId).Where(x => x.OrderSatatus == "Requested").ToList();

            Orders orders = db.orders.Find(id);
            var offers = db.offers.Where(x => x.Order_Id == id).Where(x => x.OfferSatatus == "Pending").ToList();

            OrdersViewModel viewmodel = new OrdersViewModel
            {
                order = orders,
                Offers = offers
            };

            return View(viewmodel);
        }

        // GET: Offer/AcceptOffer/5
        public ActionResult AcceptOffer()
        {
            return View();
        }

        // POST: Offer/AcceptOffer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AcceptOffer(int id)
        {
            var Offermodel = RetriveOfferByID(id);
            Offermodel.OfferSatatus = "Accepted";
            UpdateModel<Offers>(Offermodel);
            db.SaveChanges();

            var OrderModel = RetriveOrderByID(Offermodel.Order_Id);
            OrderModel.OrderSatatus = "InProgress";
            UpdateModel<Orders>(OrderModel);
            db.SaveChanges();

            return RedirectToAction("ViewOldOrders");
        }

        public Offers RetriveOfferByID(int ID)
        {
            var model = db.offers.FirstOrDefault(x => x.Offer_Id == ID);
            return model;
        }

        public Orders RetriveOrderByID(int ID)
        {
            var model = db.orders.FirstOrDefault(x => x.Order_Id == ID);
            return model;
        }

        public ActionResult RecievedOrder()
        {
            return View();
        }

        // POST: Order/ConfirmBuy/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecievedOrder(int id)
        {
            var OrderModel = RetriveOrderByID(id);
            OrderModel.OrderSatatus = "Recieved";
            UpdateModel<Orders>(OrderModel);
            db.SaveChanges();

            return RedirectToAction("IndexDelivered");
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