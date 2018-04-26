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
    public class BooksCategoriesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: BooksCategories
        public ActionResult Index()
        {
            return View(db.BooksCategorys.ToList());
        }

        // GET: BooksCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksCategory booksCategory = db.BooksCategorys.Find(id);
            if (booksCategory == null)
            {
                return HttpNotFound();
            }
            return View(booksCategory);
        }

        // GET: BooksCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] BooksCategory booksCategory)
        {
            if (ModelState.IsValid)
            {
                db.BooksCategorys.Add(booksCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booksCategory);
        }

        // GET: BooksCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksCategory booksCategory = db.BooksCategorys.Find(id);
            if (booksCategory == null)
            {
                return HttpNotFound();
            }
            return View(booksCategory);
        }

        // POST: BooksCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] BooksCategory booksCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksCategory);
        }

        // GET: BooksCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksCategory booksCategory = db.BooksCategorys.Find(id);
            if (booksCategory == null)
            {
                return HttpNotFound();
            }
            return View(booksCategory);
        }

        // POST: BooksCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksCategory booksCategory = db.BooksCategorys.Find(id);
            db.BooksCategorys.Remove(booksCategory);
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
