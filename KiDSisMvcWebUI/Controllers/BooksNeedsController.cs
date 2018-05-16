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
            List<SchoolsCategory> sc = db.SchoolsCategorys.ToList();
            List<BooksCategory> _booksCategory = db.BooksCategorys.ToList();
            ShoolBooksNeedsViewModel wm = new ShoolBooksNeedsViewModel();
            List<ShoolBooksNeedsViewModel> wmlist = new List<ShoolBooksNeedsViewModel>();

            foreach (var item in bkn)
            {
                wm.Id = item.Id;
                wm.Name = item.Name;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;
                wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wm.BookCount = item.BookCount;
                    //bkn.FirstOrDefault(x => x.Id == item.Id).BookCount;
                /* wm.SchoolsCategory =*/ /*sc.FirstOrDefault(x => x.Id == item.Id).Category;*/
                wm.SchoolsCategory = db.SchoolsCategorys.FirstOrDefault(x => x.Id == item.Id).Category;
                wmlist.Add(wm);


            }


            //wm.Id = bk[0].Id;
            //wm.Name = bk[0].Name;
            //wm.Class = bk[0].Class;
            ////wm.BookCount = bkn[0].BookCount;
            //wm.Category = sc[0].Category;
            //wm.Id = bk[0].Id;
            //wm.Name = bk[0].Name;
            //wm.Class = bk[0].Class;
            ////wm.BookCount = bkn[0].BookCount;
            //wm.BookCategory = sc[0].Category;
            //wmlist.Add(wm);



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
            //veri tabanındaki bir sütunu listye atıyor.
            List<string> SchoolCategoryList = db.SchoolsCategorys.Select(x => x.Category).ToList();

            ViewBag.ShoolListViewBag = SchoolCategoryList;


            List<string> BookNameList = db.Books.Select(x => x.Name).ToList();

            ViewBag.BookNameListViewBag = BookNameList;

            List<string> BookClassList = db.Books.Select(x => x.Class).ToList();

            ViewBag.BookClassListViewBag = BookClassList;

            return View();
        }

        // POST: BooksNeeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCount,BookId,UserId,Name")] BooksNeed booksNeed)
        {
            //aranan kod süper satır. isimleri karşılaştırıp id yi ekliyor.
            booksNeed.BookId = db.Books.FirstOrDefault(x => x.Name == booksNeed.Name).Id;

            booksNeed.UserId = 1;
            if (ModelState.IsValid)
            {
                db.BooksNeeds.Add(booksNeed);
                //db.Entry(booksNeed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksNeed);

            //BooksNeed bkcn = new BooksNeed();
            //bkcn.BookCount = booksNeed.BookCount;

            //Book Bk = new Book();

            //Bk.Name = booksNeed.Name;
            //Bk.Class = booksNeed.Class;


            //if (ModelState.IsValid)
            //{

            //    db.Books.Add(Bk);
            //    db.BooksNeeds.Add(bkcn);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(booksNeed);
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
