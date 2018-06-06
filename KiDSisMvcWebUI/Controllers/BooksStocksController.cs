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
            return View(wmlist.Where(x => x.SchoolsCategory.ToString() == schoolType));
            //return View(wmlist);
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
            List<string> SchoolCategoryList = db.SchoolsCategorys.Select(x => x.Category).ToList();

            ViewBag.ShoolListViewBag = SchoolCategoryList;


            List<string> BookNameList = db.Books.Select(x => x.Name).ToList();

            ViewBag.BookNameListViewBag = BookNameList;

            List<string> BookClassList = db.Books.Select(x => x.Class).ToList();

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
            booksStock.BookId = db.Books.FirstOrDefault(x => x.Name == booksStock.Name).Id;
            booksStock.DemandDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");            
            booksStock.UserId = Session["ManagerId"].ToString();
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
