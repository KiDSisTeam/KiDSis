using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using KiDSisMvcWebUI.Entity;
using KiDSisMvcWebUI.Models;

namespace KiDSisMvcWebUI.Controllers
{
    public class BooksDeliveriesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: BooksDeliveries
        //public ActionResult Index()
        //{
        //    return View(db.BooksDeliverys.ToList());
        //}
        // GET: BooksNeeds
        public ActionResult Index(string text)
        {
            ViewBag.KayıtHata1 = "";
            ViewBag.KayıtHata2 = "";

            if (TempData["Control"] != null) { 


                if (TempData["Control"] == "1")
            {
                ViewBag.KayıtHata1 = " Girmiş olduğunuz kitap sayısı okulun ihtiyacından fazla olamaz!";
                
            }
                if (TempData["Control"] == "2")
                {
                    ViewBag.KayıtHata2 = " Depoda bu kadar kitap yok!";

                }


            }


            ViewBag.selected = text;
            List<Book> bk = new List<Book>();
            bk = db.Books.ToList();
            List<BooksNeed> bkn = db.BooksNeeds.ToList();
            List<SchoolsCategory> sc = db.SchoolsCategorys.ToList();
            List<BooksCategory> _booksCategory = db.BooksCategorys.ToList();
            List<BooksStock> bkstk = db.BooksStocks.ToList();
            List<ShoolBooksNeedsViewModel> wmlist = new List<ShoolBooksNeedsViewModel>();
            List<LookUpDto> LookUpDto = new List<LookUpDto>();
            var Bookneed = db.BooksNeeds.Select(x => x.ShoolName).Distinct().ToList();
            string parameters = Bookneed[0];
            ViewBag.ShoolListViewBag = Bookneed;
            foreach (var item in bkn)
            {
                // bu model foreach içinde eklenmeli
                ShoolBooksNeedsViewModel wm = new ShoolBooksNeedsViewModel();
                wm.Id = item.Id;
                wm.BookId = item.BookId;
                wm.UserId = item.UserId;
                wm.ShoolName = item.ShoolName;
                wm.Name = bk.FirstOrDefault(x => x.Id == item.BookId).Name;
                wm.DemandDate = item.DemandDate;
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
                wm.SchoolsCategory = db.Books.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wmlist.Add(wm);


            }
            if (text != null && text != string.Empty)
            {
                parameters = text;
            }
            var test = wmlist.Where(x => x.ShoolName.Trim() == parameters.Trim());
            //kişinin kendi eklediği kayıtları görmesi sağlandı
            return View(wmlist.Where(x => x.ShoolName.Trim() == parameters.Trim()));
        }
        [HttpPost]
        public ActionResult Index(string list, string BookIdList, string Schols)
        {
            List<BooksStock> bkstk = db.BooksStocks.ToList();
            ShoolBooksNeedsViewModel wm = new ShoolBooksNeedsViewModel();

            string[] CounterCount = BookIdList.Split(',');
            for (int i = 0; i < CounterCount.Count(); i++)
            {
                string item = BookIdList.Split(',')[i];
                string Count = list.Split(',')[i];
                var BookNeeds = db.BooksNeeds.FirstOrDefault(x => x.BookId.ToString() == item && x.ShoolName == Schols);
                var BookStocks = db.BooksStocks.FirstOrDefault(x => x.BookId.ToString() == item);
                if (BookNeeds != null)
                {
                    if (Count == "")
                    {
                        Count = "0";
                    }

                    if (Convert.ToInt32(Count) < 1)
                    {
                        Count = "0";
                    }

                    if (BookStocks.BookCount < Convert.ToInt32(Count))
                    {
                        TempData["Control"] = "2";
                        return RedirectToAction("Index", new RouteValueDictionary(
               new { controller = "BooksDeliveries", action = "Index" }));

                    }




                    ViewBag.KayıtHata = "";
                    if (BookNeeds.BookCount < Convert.ToInt32(Count))
                    {
                        TempData["Control"] = "1";
                        //ViewBag.KayıtHata = " Girmiş olduğunuz kitap sayısı okulun ihtiyacından fazla olamaz!";
                        return RedirectToAction("Index", new RouteValueDictionary(
               new { controller = "BooksDeliveries", action = "Index" }));

                    }

                    //     ViewBag.KayıtHata = "";
                    //     if ((bkstk.FirstOrDefault(x => x.Id ==item.)) == null)
                    //     {
                    //         TempData["Control"] = "1";

                    //         return RedirectToAction("Index", new RouteValueDictionary(
                    //new { controller = "BooksDeliveries", action = "Index" }));



                    //     }



                    //     if (Count == "")
                    //     {
                    //         Count = "0";
                    //     }



                    //ViewBag.KayıtHata = "";
                    //BooksNeed bookNeedControl = new BooksNeed();
                    ////bookNeedControl = db.BooksNeeds.FirstOrDefault(x => x.BookId == booksNeed.BookId);
                    //bookDeliveriesControl = db.BooksNeeds(x => x. == booksNeed.BookId);
                    //if (bookNeedControl != null)
                    //{
                    //    TempData["Control"] = "1";

                    //    return RedirectToAction("Edit", new RouteValueDictionary(
                    //   new { controller = "BooksNeeds", action = "Edit", Id = bookNeedControl.Id }));



                    //}


                    BookNeeds.BookCount = BookNeeds.BookCount - Convert.ToInt32(Count);
                    db.Entry(BookNeeds).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //var BookStocks = db.BooksStocks.FirstOrDefault(x => x.BookId.ToString() == item);
                if (BookStocks != null)
                {




                  



                    BookStocks.BookCount = BookStocks.BookCount - Convert.ToInt32(Count);
                    db.Entry(BookStocks).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            return RedirectToAction("Index", new RouteValueDictionary(
            new { controller = "BooksDeliveries", action = "Index" }));
        }

        public ActionResult DropdownChanged(string text)
        {


            return RedirectToAction("Index", new RouteValueDictionary(
           new { controller = "BooksDeliveries", action = "Index", text = text }));

        }

        // GET: BooksDeliveries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            if (booksDelivery == null)
            {
                return HttpNotFound();
            }
            return View(booksDelivery);
        }

        // GET: BooksDeliveries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksDeliveries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,BookCount")] BooksDelivery booksDelivery)
        {
            if (ModelState.IsValid)
            {
                db.BooksDeliverys.Add(booksDelivery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booksDelivery);
        }

        // GET: BooksDeliveries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            if (booksDelivery == null)
            {
                return HttpNotFound();
            }
            return View(booksDelivery);
        }

        // POST: BooksDeliveries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,BookCount")] BooksDelivery booksDelivery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksDelivery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksDelivery);
        }

        // GET: BooksDeliveries/Details/5
        public ActionResult BooksDeliveriesPrint(/*int? id*/)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            //if (booksDelivery == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(booksDelivery);
            return View();
        }




        // GET: BooksDeliveries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            if (booksDelivery == null)
            {
                return HttpNotFound();
            }
            return View(booksDelivery);
        }

        // POST: BooksDeliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BooksDelivery booksDelivery = db.BooksDeliverys.Find(id);
            db.BooksDeliverys.Remove(booksDelivery);
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
