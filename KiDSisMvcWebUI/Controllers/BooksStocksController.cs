using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KiDSisMvcWebUI.Entity;
using KiDSisMvcWebUI.Models;

namespace KiDSisMvcWebUI.Controllers
{
    public class BooksStocksController : Controller
    {
        private DataContext db = new DataContext();

        // GET: BooksStocks
        public ActionResult Index()
        {

            List<Book> bk = new List<Book>();
            bk = db.Books.ToList();
            List<BooksNeed> bkn = db.BooksNeeds.ToList();
            List<SchoolsCategory> sc = db.SchoolsCategorys.ToList();
            List<BooksCategory> _booksCategory = db.BooksCategorys.ToList();
            List<BooksStock> bstk = db.BooksStocks.ToList();

            List<ShoolBooksStocksViewModel> wmlist = new List<ShoolBooksStocksViewModel>();

            foreach (var item in bstk)
            {// bu model foreach içinde eklenmeli
                ShoolBooksStocksViewModel wm = new ShoolBooksStocksViewModel();
                wm.Id = item.Id;
                wm.DemandDate = item.DemandDate;
                wm.Name = bk.FirstOrDefault(x => x.Id == item.BookId).Name;
                //wm.Name = item.Name;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;
                //wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wm.BookCount = item.BookCount;

                //bkn.FirstOrDefault(x => x.Id == item.Id).BookCount;
                /* wm.SchoolsCategory =*/ /*sc.FirstOrDefault(x => x.Id == item.Id).Category;*/
                wm.SchoolsCategory = db.Books.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wmlist.Add(wm);
            }

            return View(wmlist);
        }




        // GET: BooksStocks
        public ActionResult Index2()
        {

            List<Book> bk = new List<Book>();
            bk = db.Books.ToList();
            List<BooksNeed> bkn = db.BooksNeeds.ToList();
            List<SchoolsCategory> sc = db.SchoolsCategorys.ToList();
            List<BooksCategory> _booksCategory = db.BooksCategorys.ToList();
            List<BooksStock> bstk = new List<BooksStock>();
            bstk = db.BooksStocks.ToList();

            List<ShoolBooksStocksViewModel> wmlist = new List<ShoolBooksStocksViewModel>();

            foreach (var item in bstk)
            {// bu model foreach içinde eklenmeli
                ShoolBooksStocksViewModel wm = new ShoolBooksStocksViewModel();
                wm.Id = item.Id;
                wm.DemandDate = item.DemandDate;
                wm.Name = bk.FirstOrDefault(x => x.Id == item.BookId).Name;
                //wm.Name = item.Name;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;
                //wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wm.BookCount = item.BookCount;

                //bkn.FirstOrDefault(x => x.Id == item.Id).BookCount;
                /* wm.SchoolsCategory =*/ /*sc.FirstOrDefault(x => x.Id == item.Id).Category;*/
                wm.SchoolsCategory = db.Books.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wmlist.Add(wm);
            }

            string schoolType = (Session["SchoolType"]).ToString();

            if (schoolType != "İLÇE MİLLİ EĞİTİM")
            {
                wmlist.Where(x => x.SchoolsCategory.ToString() == schoolType);
                //List<string> BookClassList = db.SchoolClasses.Where(x => x.Category == schooltype).Select(x => x.Class).Distinct().ToList();
                // ViewBag.BookClassListViewBag = BookClassList;
            }
            //else
            //{
            //    wmlist;
            //    //List<string> BookClassList = db.Books.Select(x => x.Class).ToList();

            //    //List<string> BookClassList = db.SchoolClasses.Where(x => x.Category == schooltype).Select(x => x.Class).Distinct().ToList();
            //    // ViewBag.BookClassListViewBag = BookClassList;
            //}



            //return View(wmlist.Where(x => x.SchoolsCategory.ToString() == schoolType));
            return View(wmlist);
        }



        // GET: BooksStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksStock booksStock = db.BooksStocks.Find(id);
            if (booksStock == null)
            {
                return HttpNotFound();
            }
            return View(booksStock);
        }

        // GET: BooksStocks/Create
        public ActionResult Create()
        {
            //veri tabanındaki bir sütunu listye atıyor.
            List<string> SchoolCategoryList = db.SchoolsCategorys.Select(x => x.Category).Distinct().ToList();

            ViewBag.ShoolListViewBag = SchoolCategoryList;


            List<string> BookNameList = db.SchoolClasses.Select(x => x.Class).Distinct().ToList();

            ViewBag.BookNameListViewBag = BookNameList;

            List<string> BookClassList = db.Books.Select(x => x.Class).Distinct().ToList();

            ViewBag.BookClassListViewBag = BookClassList;


            return View();
        }

        // POST: BooksStocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCount,BookId,UserId,Name")] BooksStock booksStock)
        {
            //aranan kod süper satır. isimleri karşılaştırıp id yi ekliyor.
            /*"Arapça - 5 Ders ve Öğrenci Çalışma Kitabı"*/
            booksStock.BookId = db.Books.FirstOrDefault(x => x.Id.ToString() == booksStock.Name).Id;
            booksStock.DemandDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            booksStock.UserId = Session["ManagerId"].ToString();

            ViewBag.KayıtHata = "";
            BooksStock bookNeedControl = new BooksStock();
            bookNeedControl = db.BooksStocks.FirstOrDefault(x => x.BookId == booksStock.BookId);
            if (bookNeedControl != null)
            {
                TempData["Control"] = "1";
                booksStock.Id = db.BooksStocks.FirstOrDefault(x => x.BookId == booksStock.BookId).Id;
                return RedirectToAction("Edit", new RouteValueDictionary(
               new { controller = "BooksStocks", action = "Edit", Id = booksStock.Id }));
            }



            if (ModelState.IsValid)
            {
                db.BooksStocks.Add(booksStock);
                //db.Entry(booksNeed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksStock);





            //if (ModelState.IsValid)
            //{
            //    db.BooksStocks.Add(booksStock);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(booksStock);
        }

        // GET: BooksStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.KayıtHata = "";

            if (TempData["Control"] != null)
            {
                ViewBag.KayıtHata = " Bu Kitabı daha önce eklediniz. Lütfen kitap sayısını güncelleyiniz!";
            }


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksStock booksStock = db.BooksStocks.Find(id);
            if (booksStock == null)
            {
                return HttpNotFound();
            }
            return View(booksStock);
        }

        // POST: BooksStocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookCount,BookId,DemandDate,Name,UserId")] BooksStock booksStock)
        {
            if (ModelState.IsValid)
            {
                booksStock.DemandDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                db.Entry(booksStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksStock);
        }

        // GET: BooksStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksStock booksStock = db.BooksStocks.Find(id);
            if (booksStock == null)
            {
                return HttpNotFound();
            }
            return View(booksStock);
        }

        // POST: BooksStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksStock booksStock = db.BooksStocks.Find(id);
            db.BooksStocks.Remove(booksStock);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetClass(string Category)
        {

            var entityList = new List<Book>();
            List<Book> book = db.Books.Where(x => x.BookType == Category).OrderByDescending(x => x.Class).ToList();

            var Custom = book.Select(x => new { x.Class }).GroupBy(x => x.Class).ToList();
            foreach (var item in Custom)
            {
                var DbControl = db.Books.FirstOrDefault(x => x.Class == item.Key);
                var entity = new Book();
                entity.Id = DbControl.Id;
                entity.Class = DbControl.Class;
                entityList.Add(entity);
            }



            //var list = book.Select(x => new { x.Class, x.Id }).Distinct().ToList();
            return Json(entityList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBookName(string Class)
        {
            List<Book> bookName = db.Books.Where(x => x.Class == Class).ToList();
            var list = bookName.Select(x => new { x.Id, x.Name }).Distinct().ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
