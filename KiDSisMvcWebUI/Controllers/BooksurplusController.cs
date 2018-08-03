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
    public class BooksurplusController : Controller
    {
        private const string Format = "dd/MM/yyyy HH:mm:ss";
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
                wm.UserId = item.UserId;
                wm.BookId = item.BookId;
                wm.Name = bk.FirstOrDefault(x => x.Id == item.BookId).Name;
                wm.DemandDate = item.DemandDate;
                //wm.Name = item.Name;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;
                //wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wm.BookCount = item.BookCount;

                //bkn.FirstOrDefault(x => x.Id == item.Id).BookCount;
                /* wm.SchoolsCategory =*/ /*sc.FirstOrDefault(x => x.Id == item.Id).Category;*/
                wm.SchoolsCategory = db.Books.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wmlist.Add(wm);
            }
            string managerId = (Session["ManagerId"]).ToString();
            return View(wmlist.Where(x => x.UserId.ToString() == managerId));
            //return View(wmlist);
        }



        public ActionResult Index2()
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
                wm.SchoolName = item.SchoolName;
                wm.UserId = item.UserId;
                wm.Name = bk.FirstOrDefault(x => x.Id == item.BookId).Name;
                wm.DemandDate = item.DemandDate;
                //wm.Name = item.Name;
                wm.Class = bk.FirstOrDefault(x => x.Id == item.BookId).Class;
                //wm.BookCategory = bk.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wm.BookCount = item.BookCount;

                //bkn.FirstOrDefault(x => x.Id == item.Id).BookCount;
                /* wm.SchoolsCategory =*/ /*sc.FirstOrDefault(x => x.Id == item.Id).Category;*/
                wm.SchoolsCategory = db.Books.FirstOrDefault(x => x.Id == item.BookId).BookType;
                wmlist.Add(wm);
            }
            string managerId = (Session["ManagerId"]).ToString();
            //return View(wmlist.Where(x => x.UserId.ToString() == managerId));
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
            //List<string> SchoolCategoryList = db.SchoolsCategorys.Select(x => x.Category).ToList();

            ViewBag.ShoolListViewBag = Session["SchoolType"].ToString();

            string schooltype = Session["SchoolType"].ToString();
            List<string> BookNameList = db.Books.Where(x => x.BookType == schooltype).Select(x => x.Name).ToList();

            ViewBag.BookNameListViewBag = BookNameList;



            //List<string> BookClassList = db.Books.Select(x => x.Class).ToList();

            //string schooltype = Session["SchoolType"].ToString();
            List<string> BookClassList = db.Books.Where(x => x.BookType == schooltype).Select(x => x.Class).Distinct().ToList();
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
            //aranan kod süper satır. isimleri karşılaştırıp id yi ekliyor
            booksurplus.BookId = Convert.ToInt32(booksurplus.Name);
           // booksurplus.BookId = db.Books.FirstOrDefault(x => x.Name == booksurplus.Name).Id;
                     booksurplus.UserId = Session["ManagerId"].ToString();
            booksurplus.SchoolName = Session["SchoolName"].ToString();
            booksurplus.DemandDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            booksurplus.Id = db.Booksurplus.FirstOrDefault(x => x.BookId == booksurplus.BookId).Id;
            ViewBag.KayıtHata = "";
            Booksurplus booksurplusControl = new Booksurplus();
            booksurplusControl = db.Booksurplus.FirstOrDefault(x => x.BookId == booksurplus.BookId);
            if (booksurplusControl != null)
            {
               TempData["Control"] = "1";
                return RedirectToAction("Edit", new RouteValueDictionary(
               new { controller = "Booksurplus", action = "Edit", Id=booksurplus.Id  }));

            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Booksurplus.Add(booksurplus);
                    //db.Entry(booksNeed).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(booksurplus);


            }



        }

        // GET: Booksurplus/Edit/5
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


            //Booksurplus booksurplus = db.Booksurplus.Find(id);

            Booksurplus booksurplus = db.Booksurplus.Where(x => x.Id == id).FirstOrDefault();
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
        public ActionResult Edit([Bind(Include = "Id,BookCount,BookId,UserId,Name,SchoolName,DemandDate")] Booksurplus booksurplus)
        {
            booksurplus.DemandDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            if (ModelState.IsValid)
            {
                booksurplus.DemandDate = DateTime.Now.ToString(Format);
                db.Entry(booksurplus).State = EntityState.Modified;
               // db.Booksurplus.Add(booksurplus);
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
