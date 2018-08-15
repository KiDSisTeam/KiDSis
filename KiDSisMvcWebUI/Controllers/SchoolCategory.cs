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
    public class SchoolCategory : Controller
    {
        private DataContext db = new DataContext();

        // GET: SchoolCategory
        public ActionResult Index()
        {
            return View(db.SchoolsCategorys.ToList());
        }

        // GET: SchoolCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolsCategory schoolsCategory = db.SchoolsCategorys.Find(id);
            if (schoolsCategory == null)
            {
                return HttpNotFound();
            }
            return View(schoolsCategory);
        }

        // GET: SchoolCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Category")] SchoolsCategory schoolsCategory)
        {
            if (ModelState.IsValid)
            {
                db.SchoolsCategorys.Add(schoolsCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoolsCategory);
        }

        // GET: SchoolCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolsCategory schoolsCategory = db.SchoolsCategorys.Find(id);
            if (schoolsCategory == null)
            {
                return HttpNotFound();
            }
            return View(schoolsCategory);
        }

        // POST: SchoolCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category")] SchoolsCategory schoolsCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolsCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolsCategory);
        }

        // GET: SchoolCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolsCategory schoolsCategory = db.SchoolsCategorys.Find(id);
            if (schoolsCategory == null)
            {
                return HttpNotFound();
            }
            return View(schoolsCategory);
        }

        // POST: SchoolCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolsCategory schoolsCategory = db.SchoolsCategorys.Find(id);
            db.SchoolsCategorys.Remove(schoolsCategory);
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
