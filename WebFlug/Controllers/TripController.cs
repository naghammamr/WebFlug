﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebFlug.Models;

namespace WebFlug.Controllers
{
    public class TripController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trip
        [Authorize(Roles = "Admin")]
        public IEnumerable<Trips> GetTrips()
        {
            var trips = db.trips.ToList();
            return trips;
        }

        //Index trip
        public ActionResult Index()
        {
            var orders = GetTrips();
            return View(orders);
        }

        // GET: Trip/Create
        public ActionResult Create()
          {
              return View();
          }

        // POST: Trip/Create
        [HttpPost]
        [ValidateAntiForgeryToken]//prevent cross-site request forgery attacks
        public ActionResult Create(Trips trip, HttpPostedFileBase upload)
        {
            var userID = User.Identity.GetUserId();

              if (ModelState.IsValid)
              {
                trip.UserId = userID;

                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                trip.TicketPhoto = upload.FileName;

                trip.TripCreationDate = DateTime.Now;

                db.trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("MyTrips");
              }

              return View(trip);
          }

        //Get : user trips 
        public ActionResult MyTrips()
        {
            var UserId = User.Identity.GetUserId();
            var trips = db.trips.Where(a => a.UserId == UserId);
            return View(trips.ToList());
        }

        // GET: Trip/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trips trip = db.trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trip/Details/5
        public ActionResult VisitorDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trips trip = db.trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Trip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trips trip = db.trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trip/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Trips trip, int id, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    trip.TicketPhoto = upload.FileName;
                }
                trip.Trip_Id = id;                
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MyTrips");
            }
            return View(trip);
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trips trip = db.trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trips trip = db.trips.Find(id);
            db.trips.Remove(trip);
            db.SaveChanges();
            return RedirectToAction("MyTrips");
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
