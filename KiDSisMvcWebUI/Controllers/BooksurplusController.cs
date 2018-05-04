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
    public class BooksurplusController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Booksurplus
        public ActionResult Index()
        {
            return View(db.Booksurplus.ToList());
        }

        // GET: Booksurplus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booksurplus booksurplus = db.Booksurplus.Find(id);
            if (booksurplus == null)
            {
                return HttpNotFound();
            }
            return View(booksurplus);
        }

        // GET: Booksurplus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Booksurplus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCount,BookId,UserId")] Booksurplus booksurplus)
        {
            if (ModelState.IsValid)
            {
                db.Booksurplus.Add(booksurplus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booksurplus);
        }

        // GET: Booksurplus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booksurplus booksurplus = db.Booksurplus.Find(id);
            if (booksurplus == null)
            {
                return HttpNotFound();
            }
            return View(booksurplus);
        }

        // POST: Booksurplus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookCount,BookId,UserId")] Booksurplus booksurplus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksurplus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksurplus);
        }

        // GET: Booksurplus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booksurplus booksurplus = db.Booksurplus.Find(id);
            if (booksurplus == null)
            {
                return HttpNotFound();
            }
            return View(booksurplus);
        }

        // POST: Booksurplus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booksurplus booksurplus = db.Booksurplus.Find(id);
            db.Booksurplus.Remove(booksurplus);
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
