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

            List<Book> bk = new List<Book>();
            bk = db.Books.ToList();
            List<BooksNeed> bkn = db.BooksNeeds.ToList();
            List<SchoolsCategory> sc = db.SchoolsCategorys.ToList();
            List<BooksCategory> _booksCategory = db.BooksCategorys.ToList();
            List<BooksStock> bstk = db.BooksStocks.ToList();
            List<Booksurplus> bksrp = db.Booksurplus.ToList();

            List<ShoolBooksurplusViewModel> wmlist = new List<ShoolBooksurplusViewModel>();

            foreach (var item in bksrp)
            {// bu model foreach içinde eklenmeli
                ShoolBooksurplusViewModel wm = new ShoolBooksurplusViewModel();
                wm.Id = item.Id;
                wm.Name = bk.FirstOrDefault(x => x.Id == item.BookId).Name;
                //wm.Name = item.Name;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;
                //wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wm.BookCount = item.BookCount;

                //bkn.FirstOrDefault(x => x.Id == item.Id).BookCount;
                /* wm.SchoolsCategory =*/ /*sc.FirstOrDefault(x => x.Id == item.Id).Category;*/
                wm.SchoolsCategory = db.SchoolsCategorys.FirstOrDefault(x => x.Id == item.BookId).Category;
                wmlist.Add(wm);
            }
            return View(wmlist);




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
            //veri tabanındaki bir sütunu listye atıyor.
            List<string> SchoolCategoryList = db.SchoolsCategorys.Select(x => x.Category).ToList();

            ViewBag.ShoolListViewBag = SchoolCategoryList;


            List<string> BookNameList = db.Books.Select(x => x.Name).ToList();

            ViewBag.BookNameListViewBag = BookNameList;

            List<string> BookClassList = db.Books.Select(x => x.Class).ToList();

            ViewBag.BookClassListViewBag = BookClassList;


            return View();
        }

        // POST: Booksurplus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCount,BookId,UserId,Name")] Booksurplus booksurplus)
        {
            //aranan kod süper satır. isimleri karşılaştırıp id yi ekliyor.
            booksurplus.BookId = db.Books.FirstOrDefault(x => x.Name == booksurplus.Name).Id;

            booksurplus.UserId = 1;
            if (ModelState.IsValid)
            {
                db.Booksurplus.Add(booksurplus);
                //db.Entry(booksNeed).State = EntityState.Modified;
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
