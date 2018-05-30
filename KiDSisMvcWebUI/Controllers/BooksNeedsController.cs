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
            List<Book> bk = new List<Book>();
            bk = db.Books.ToList();
            List<BooksNeed> bkn = db.BooksNeeds.ToList();
            List<SchoolsCategory> sc = db.SchoolsCategorys.ToList();
            List<BooksCategory> _booksCategory = db.BooksCategorys.ToList();

            List<ShoolBooksNeedsViewModel> wmlist = new List<ShoolBooksNeedsViewModel>();

            foreach (var item in bkn)
            {// bu model foreach içinde eklenmeli
                ShoolBooksNeedsViewModel wm = new ShoolBooksNeedsViewModel();
                wm.Id = item.Id;
                wm.UserId = item.UserId;
                //wm.BookId=
                wm.Name = bk.FirstOrDefault(x => x.Id == item.BookId).Name;
                wm.Name = item.Name;
                //wm.BookCode= bk.FirstOrDefault(x => x.Id == item.BookId).Code;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;
                wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wm.BookCount = item.BookCount;
                //bkn.FirstOrDefault(x => x.Id == item.Id).BookCount;
                /* wm.SchoolsCategory =*/ /*sc.FirstOrDefault(x => x.Id == item.Id).Category;*/
                wm.SchoolsCategory = db.SchoolsCategorys.FirstOrDefault(x => x.Id == item.BookId).Category;
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
            //kişinin kendi eklediği kayıtları görmesi sağlandı
            string managerId = (Session["ManagerId"]).ToString();
            return View(wmlist.Where(x => x.UserId.ToString() == managerId));
            // return View(wmlist);
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


            List<Book> BookNameList = db.Books.ToList();

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
            //booksNeed.BookId = db.Books.FirstOrDefault(x => x.Name == booksNeed.Name).Id;

            booksNeed.BookId = Convert.ToInt32(booksNeed.Name);

            booksNeed.UserId = Session["ManagerId"].ToString();

            //sorular.MangerId = Convert.ToInt32(Session["MangerId"]);
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



        public ActionResult SchoolsCategories()
        {
            List<SchoolsCategory> Category = db.SchoolsCategorys.ToList();
            return Json(Category, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClass(string BookType)
        {
            List<Book> book = db.Books.Where(x => x.BookType == BookType).OrderByDescending(x => x.Name).ToList();
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBookName(string Class)
        {
            List<Book> bookName = db.Books.Where(x => x.Class == Class).ToList();
            return Json(bookName, JsonRequestBehavior.AllowGet);
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
