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
    public class BooksNeedsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: BooksNeeds
        public ActionResult Index()
        {

            List<Book> bk = db.Books.ToList();
            List<BooksNeed> bkn = db.BooksNeeds.ToList();

            ShoolBooksNeedsViewModel wm = new ShoolBooksNeedsViewModel();
            List<ShoolBooksNeedsViewModel> wmlist = new List<ShoolBooksNeedsViewModel>();
            wm.Id = bk[0].Id;
            wm.Name = bk[0].Name;
            wm.Class = bk[0].Class;
            wm.BookCount = bkn[0].BookCount;

            wmlist.Add(wm);



            return View(wmlist);
        }

        // GET: BooksNeeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksNeed booksNeed = db.BooksNeeds.Find(id);
            if (booksNeed == null)
            {
                return HttpNotFound();
            }
            return View(booksNeed);
        }

        // GET: BooksNeeds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksNeeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCount")] BooksNeed booksNeed)
        {
            if (ModelState.IsValid)
            {
                db.BooksNeeds.Add(booksNeed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booksNeed);
        }

        // GET: BooksNeeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksNeed booksNeed = db.BooksNeeds.Find(id);
            if (booksNeed == null)
            {
                return HttpNotFound();
            }
            return View(booksNeed);
        }

        // POST: BooksNeeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookCount")] BooksNeed booksNeed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksNeed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksNeed);
        }

        // GET: BooksNeeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksNeed booksNeed = db.BooksNeeds.Find(id);
            if (booksNeed == null)
            {
                return HttpNotFound();
            }
            return View(booksNeed);
        }

        // POST: BooksNeeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksNeed booksNeed = db.BooksNeeds.Find(id);
            db.BooksNeeds.Remove(booksNeed);
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
