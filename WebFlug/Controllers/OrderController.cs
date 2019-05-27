using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Orders order, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                order.Email = User.Identity.Name;

                string number = GetUniqueKey(6);
                order.OrderNumber = number;

                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                order.ProductImage = upload.FileName;

                order.CreationDate = DateTime.Now;

                order.OrderSatatus = "Requested";

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

                order.CreationDate = DateTime.Now;

                order.Email = User.Identity.Name;

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