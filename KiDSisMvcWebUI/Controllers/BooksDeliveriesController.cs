using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
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
        RedirecteViewModel model = new RedirecteViewModel();
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

            if (TempData["Control"] != null)
            {


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





                    var delivery = new BooksDelivery();


                    BookStocks.BookCount = BookStocks.BookCount - Convert.ToInt32(Count);
                    db.Entry(BookStocks).State = EntityState.Modified;
                    db.SaveChanges();

                    delivery.CreateDate = DateTime.Now;
                    delivery.Year = DateTime.Now.Year;
                    delivery.UpdateDate = DateTime.Now;
                    delivery.SchoolsName = Schols;
                    delivery.BookCount = Convert.ToInt32(Count);
                    delivery.Deliverer = "Tutanak Düzenlenmedi";
                    delivery.Recipient = "Tutanak Düzenlenmedi";
                    delivery.BookId = BookNeeds.BookId;
                    db.Entry(delivery).State = EntityState.Added;
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

        public ActionResult Dropdownreport(string text, string date1, string date2)
        {
            TempData["date1"] = date1;
            TempData["date2"] = date2;
            return RedirectToAction("Index1", "BooksDeliveries", new { text = text, date1 = date1, date2 = date2 });

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

        public ActionResult Index1(string text, string Schols, string date1, string date2, string DeliveredName, string Recipedname, bool update = false)


        {
            //if (date1 != null)
            //{
            //    DateTime dt = DateTime.ParseExact(date1.ToString(), "MM.dd.yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            //    string s = dt.ToString("dd.M.yyyy", CultureInfo.InvariantCulture);
            //}
            //if (date2 != null)
            //{
            //    DateTime dt1 = DateTime.ParseExact(date2.ToString(), "MM.dd.yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
            //    string s1 = dt1.ToString("dd.M.yyyy", CultureInfo.InvariantCulture);

            //}



            if (string.IsNullOrEmpty(text))
            {
                text = Schols;
            }


            ViewBag.KayıtHata1 = "";
            ViewBag.KayıtHata2 = "";
            //ViewBag.SchoolName = "";
            //date1 = TempData["date1"] != null ? (DateTime)TempData["date1"] : DateTime.Now;
            //date2 = TempData["date2"] != null ? (DateTime)TempData["date2"] : DateTime.Now;
            //ekrandan göndermede sıkıntı olduğu için şimdilik default değer atandı sorun çözülene kadar böyle devam edilecek

            ViewBag.selected = text;
            List<Book> bk = new List<Book>();
            bk = db.Books.ToList();
            List<BooksDelivery> bkn = db.BooksDeliverys.Where(x => x.SchoolsName == text).ToList();
            List<SchoolsCategory> sc = db.SchoolsCategorys.ToList();
            List<BooksCategory> _booksCategory = db.BooksCategorys.ToList();
            List<BooksDelivery> bkstk = db.BooksDeliverys.ToList();
            List<BooksDeliveryViewModel> wmlist = new List<BooksDeliveryViewModel>();
            List<LookUpDto> LookUpDto = new List<LookUpDto>();
            var Bookneed = db.BooksNeeds.Select(x => x.ShoolName).Distinct().ToList();
            string parameters = Bookneed[0];
            ViewBag.ShoolListViewBag = Bookneed;

            foreach (var item in bkn)
            {
                // bu model foreach içinde eklenmeli
                BooksDeliveryViewModel wm = new BooksDeliveryViewModel();
                wm.Id = item.Id;
                wm.BookId = item.BookId;
                wm.CreateDate = item.CreateDate;
                //wm.UserId = item.UserId;
                wm.SchoolsName = item.SchoolsName;
                wm.Deliverer = bkn.LastOrDefault(x => x.BookId == item.BookId).Deliverer;
                wm.Recipient = bkn.LastOrDefault(x => x.BookId == item.BookId).Recipient;
                //ViewBag.SchoolName = item.SchoolsName;
                //ViewBag.TeslimEden = wm.Deliverer;
                //ViewBag.TeslimAlan = wm.Recipient;
                wm.Name = bk.FirstOrDefault(x => x.Id == item.BookId).Name;
                //wm.DemandDate = item.DemandDate;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;

                //wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;
                // stoktaki kitap sayısını bulmak için çalışıldı.
                if ((bkstk.FirstOrDefault(x => x.BookId == item.BookId)) == null)
                {
                    wm.BookCount = 0;

                }
                else
                {
                    wm.BookCount = bkstk.FirstOrDefault(x => x.BookId == item.BookId).BookCount;

                }

                //wm.BookCount = item.BookCount;
                wm.SchoolsCategory = db.Books.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wmlist.Add(wm);
            }
            if (text != null && text != string.Empty)
            {
                parameters = text;
            }

            var DateFilte1 = new DateTime();
            var DateFilter2 = new DateTime();
            DateFilte1 = DateTime.Now;
            DateFilter2 = DateTime.Now;

            // Sorun olduğu için yoruma alınmıştı.
            DateFilte1 = new DateTime(DateFilte1.Year, DateFilte1.Month, DateFilte1.Day, 0, 0, 0);


            if (!string.IsNullOrEmpty(date1))
            {
                string[] words = date1.Split('.');
                DateFilte1 = new DateTime(Convert.ToInt32(words[2].Split(' ')[0]), Convert.ToInt32(words[1]), Convert.ToInt32(words[0]), 0, 0, 0);
            }
            if (!string.IsNullOrEmpty(date2))
            {
                string[] words = date2.Split('.');
                DateFilter2 = new DateTime(Convert.ToInt32(words[2].Split(' ')[0]), Convert.ToInt32(words[1]), Convert.ToInt32(words[0]), 0, 0, 0);
            }

            DateFilte1 = new DateTime(DateFilte1.Year, DateFilte1.Month, DateFilte1.Day, 0, 0, 0);
            DateFilter2 = new DateTime(DateFilter2.Year, DateFilter2.Month, DateFilter2.Day, 0, 0, 0);
            //DateTime temp = DateTime.ParseExact("11/02/16", "dd/MM/yy", CultureInfo.InvariantCulture);

            TempData["date1"] = DateFilte1;
            TempData["date2"] = DateFilter2;
            //parameters.Trim()
            var BooksDeliveryList = wmlist.Where(x => x.SchoolsName.Trim() == text && x.CreateDate >= DateFilte1 && x.CreateDate <= DateFilter2);

            if (update)
            {
                foreach (var UpdateItem in BooksDeliveryList)
                {
                    BooksDelivery updateEntity = new BooksDelivery();
                    var Entity = db.BooksDeliverys.FirstOrDefault(x => x.Id == UpdateItem.Id);
                    Entity.Id = UpdateItem.Id;
                    Entity.Recipient = Recipedname;
                    Entity.Deliverer = DeliveredName;
                    Entity.UpdateDate = DateTime.Now;
                    db.Entry(Entity).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            //Aynı kitaptan istenirse gruplama yapılıyor
            var results = (from ssi in BooksDeliveryList
                           group ssi by new { ssi.Class, ssi.Name } into g
                           select new { Class = g.Key.Class, Name = g.Key.Name, BookCount = g.Sum(x => x.BookCount) }).ToList();

            List<BooksDeliveryViewModel> wmlistresult = new List<BooksDeliveryViewModel>();
            foreach (var item in results)
            {
                BooksDeliveryViewModel wmresult = new BooksDeliveryViewModel();

                wmresult.BookCount = item.BookCount;
                wmresult.Class = item.Class;
                wmresult.Name = item.Name;
                wmlistresult.Add(wmresult);

            }
            List<BooksDelivery> ResultBookDeliveries = db.BooksDeliverys.Where(x => x.SchoolsName == text).ToList();
            if (ResultBookDeliveries.Count() != 0 && !string.IsNullOrEmpty(text))
            {
                ViewBag.SchoolName = ResultBookDeliveries.Where(x => x.SchoolsName == text).OrderByDescending(x => x.UpdateDate).FirstOrDefault().SchoolsName;
                ViewBag.TeslimEden = ResultBookDeliveries.Where(x => x.SchoolsName == text).OrderByDescending(x => x.UpdateDate).FirstOrDefault().Deliverer;
                ViewBag.TeslimAlan = ResultBookDeliveries.Where(x => x.SchoolsName == text).OrderByDescending(x => x.UpdateDate).FirstOrDefault().Recipient;
            }

            return View(wmlistresult);
            //kişinin kendi eklediği kayıtları görmesi sağlandı
            //return View(wmlist.Where(x => x.SchoolsName.Trim() == parameters.Trim() && x.CreateDate >= DateFilte1 && x.CreateDate <= DateFilter2));
        }

        [HttpPost]
        public ActionResult DeliveryPost(string Schols, string DeliveredName, string Recipedname, string date_ex, string date_exx)
        {

            return RedirectToAction("Index1", "BooksDeliveries", new { text = Schols, date1 = date_ex, date2 = date_exx, DeliveredName = DeliveredName, Recipedname = Recipedname, update = true });
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
