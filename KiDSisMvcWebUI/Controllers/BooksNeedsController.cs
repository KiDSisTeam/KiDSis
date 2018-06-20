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
            List<BooksStock> bkstk = db.BooksStocks.ToList();
            List<ShoolBooksNeedsViewModel> wmlist = new List<ShoolBooksNeedsViewModel>();

            foreach (var item in bkn)
            {// bu model foreach içinde eklenmeli
                ShoolBooksNeedsViewModel wm = new ShoolBooksNeedsViewModel();
                wm.Id = item.Id;
                wm.UserId = item.UserId;
                //wm.BookId=
                wm.Name = bk.FirstOrDefault(x => x.Id == item.BookId).Name;
                //wm.Name = item.Name;
                wm.DemandDate = item.DemandDate;
                //wm.BookCode= bk.FirstOrDefault(x => x.Id == item.BookId).Code;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;
                wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;
                // stoktaki kitap sayısını bulmak için çalışıldı.
                if ((bkstk.FirstOrDefault(x => x.BookId == item.BookId)) == null)
                {
                    wm.BooksStockBookCount = 0;

                }
                else
                {
                    wm.BooksStockBookCount = bkstk.FirstOrDefault(x => x.BookId == item.BookId).BookCount;

                }

                wm.BookCount = item.BookCount;
                //bkn.FirstOrDefault(x => x.Id == item.Id).BookCount;
                /* wm.SchoolsCategory =*/ /*sc.FirstOrDefault(x => x.Id == item.Id).Category;*/
                wm.SchoolsCategory = db.Books.FirstOrDefault(x => x.Id == item.BookId).BookType;
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


        public ActionResult Index2()
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
                //wm.Name = item.Name;
                wm.DemandDate = item.DemandDate;
                wm.ShoolName = item.ShoolName;
                //wm.BookCode= bk.FirstOrDefault(x => x.Id == item.BookId).Code;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;
                wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;

                wm.BookCount = item.BookCount;
                //bkn.FirstOrDefault(x => x.Id == item.Id).BookCount;
                /* wm.SchoolsCategory =*/ /*sc.FirstOrDefault(x => x.Id == item.Id).Category;*/
                wm.SchoolsCategory = db.Books.FirstOrDefault(x => x.Id == item.BookId).BookType;
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
            // return View(wmlist.Where(x => x.UserId.ToString() == managerId));
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

            // ViewBag.ShoolListViewBag = SchoolCategoryList;
            ViewBag.ShoolListViewBag = Session["SchoolType"].ToString();
            //wm.BookCategory = Session["SchoolType"].ToString();


            List<Book> BookNameList = db.Books.ToList();
            ViewBag.BookNameListViewBag = BookNameList;

            //List<string> BookClassList = db.Books.Where(x=>x.BookType== Session["SchoolType"].ToString()).Select(x => x.Class).ToList();
            string schooltype = Session["SchoolType"].ToString();
            // List<string> BookClassList = db.Books.Select(x => x.Class).ToList();
            List<string> BookClassList = db.Books.Where(x => x.BookType == schooltype).Select(x => x.Class).Distinct().ToList();

            ViewBag.BookClassListViewBag = BookClassList;


            //  List<string> BookNameList = db.Books.Select(x => x.Class== BookClassList).ToList();


            return View();
        }

        // POST: BooksNeeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCount,BookId,UserId,Name,Category")] BooksNeed booksNeed)
        {
            //aranan kod süper satır. isimleri karşılaştırıp id yi ekliyor.
            booksNeed.BookId = Convert.ToInt32(booksNeed.Name);
            //booksNeed.BookId = db.Books.FirstOrDefault(x => x.Name == booksNeed.Name).Id;
            //booksNeed.Id=db.BooksNeeds.FirstOrDefault(x => x.BookId == booksNeed.BookId).Id;
            //booksNeed.BookId = Convert.ToInt32(booksNeed.Name);
            booksNeed.ShoolName = Session["SchoolName"].ToString();
            booksNeed.UserId = Session["ManagerId"].ToString();

            booksNeed.DemandDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //DateTime.Now.ToString("dd-MM-yyyyThh:mm:sszzz");

            //DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszzz");

            //sorular.MangerId = Convert.ToInt32(Session["MangerId"]);
            ViewBag.KayıtHata = "";
            BooksNeed bookNeedControl = new BooksNeed();
            bookNeedControl = db.BooksNeeds.FirstOrDefault(x => x.BookId == booksNeed.BookId);
            if (bookNeedControl != null)
            {
                TempData["Control"] = "1";

                return RedirectToAction("Edit", new RouteValueDictionary(
               new { controller = "BooksNeeds", action = "Edit", Id=bookNeedControl.Id }));



            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.BooksNeeds.Add(booksNeed);
                    //db.Entry(booksNeed).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(booksNeed);

            }



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
            ViewBag.KayıtHata = "";

            if (TempData["Control"] != null)
            {
                ViewBag.KayıtHata = " Bu Kitabı daha önce eklediniz. Lütfen kitap sayısını güncelleyiniz!";
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BooksNeed booksNeed = db.BooksNeeds.Where(x => x.Id == id).FirstOrDefault();

            //.Find(id);




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
        public ActionResult Edit([Bind(Include = "Id,BookCount,Name,BookId,DemandDate,ShoolName,UserId")] BooksNeed booksNeed)
        {
            //booksNeed.Id = db.BooksNeeds.FirstOrDefault(x => x.BookId == booksNeed.BookId).Id;
            //booksNeed.Id = booksNeed.Id;
            //booksNeed.BookId = db.BooksNeeds.FirstOrDefault(x => x.Id == booksNeed.Id).BookId;
            //booksNeed.Name = db.BooksNeeds.FirstOrDefault(x => x.Id == booksNeed.Id).Name;
            //booksNeed.ShoolName = Session["SchoolName"].ToString();
            //booksNeed.UserId = Session["ManagerId"].ToString();
            //booksNeed.DemandDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //booksNeed.BookCount = booksNeed.BookCount;
            booksNeed.DemandDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
            string schooltype = Session["SchoolType"].ToString();
            var BookClassList = db.Books.Where(x => x.BookType == schooltype).Select(x => x.Class).Distinct().ToList();
            //List<string> BookClassList = db.Books.Where(x => x.BookType == schooltype).Select(x => x.Class).Distinct().ToList();
            //List<SchoolsCategory> lstCat = db.SchoolsCategorys.ToList();
            return Json(BookClassList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClass(string BookType)
        {
            List<Book> book = db.Books.Where(x => x.BookType == BookType).OrderByDescending(x => x.Name).ToList();
            return Json(book, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBookName(string Class)
        {
            List<Book> bookName = db.Books.Where(x => x.Class.Contains(Class)).ToList();
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
