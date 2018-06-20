using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KiDSisMvcWebUI.Entity;
using KiDSisMvcWebUI.Models;

namespace KiDSisMvcWebUI.Controllers
{
    public class BooksDeliveriesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: BooksDeliveries
        public ActionResult Index()
        {
            return View(db.BooksDeliverys.ToList());
        }

        // GET: BooksDeliveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            if (booksDelivery == null)
            {
                return HttpNotFound();
            }
            return View(booksDelivery);
        }

        // GET: BooksDeliveries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksDeliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCount")] BooksDelivery booksDelivery)
        {
            if (ModelState.IsValid)
            {
                db.BooksDeliverys.Add(booksDelivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booksDelivery);
        }

        // GET: BooksDeliveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            if (booksDelivery == null)
            {
                return HttpNotFound();
            }
            return View(booksDelivery);
        }

        // POST: BooksDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookCount")] BooksDelivery booksDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksDelivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksDelivery);
        }

        // GET: BooksDeliveries/Details/5
        public ActionResult BooksDeliveriesPrint(/*int? id*/)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            //if (booksDelivery == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(booksDelivery);
            return View();
        }




        // GET: BooksDeliveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            if (booksDelivery == null)
            {
                return HttpNotFound();
            }
            return View(booksDelivery);
        }

        // POST: BooksDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            db.BooksDeliverys.Remove(booksDelivery);
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
