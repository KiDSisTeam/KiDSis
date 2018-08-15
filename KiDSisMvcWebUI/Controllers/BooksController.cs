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
    public class BooksController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Books
        public ActionResult Index()
        {
            //BooksCategory b = new BooksCategory();
            //b = db.BooksCategorys.FirstOrDefault(x=>x.Name=="OrtaOkul");
            // Bir şarta göre sorgulama yapma kodu
            //List<Book> _boks = db.Books.Where(x => x.BooksCategorys.Name == "OrtaOkul").ToList();
            List<Book> _boks = db.Books.ToList();
            return View(_boks);
            //return View(db.Books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create(int Id = 0)
        {
            /*List<SchoolsCategory>*/

            //veri tabanındaki bir sütunu listye atıyor.
            List<string> SchoolList = db.SchoolsCategorys.Select(x => x.Category).ToList();
            ViewBag.KayıtHata = "";
            if (Id==1)
            {
                ViewBag.KayıtHata = " Bu Kayıt daha önce eklenmiştir.";
            }


            List<string> BookNameList = db.Books.Select(x => x.Name).Distinct().ToList();

            ViewBag.BookNameListViewBag = BookNameList;


            //List<string> SchoolList = new List<string>
            //{"ANAOKULU","İLKOKUL", "ORTAOKUL","LİSE","ÖZEL ÖĞRETİM"};
            ViewBag.ShoolListViewBag = SchoolList;
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Class,BookType,BooksCategoryId,Category")] Book book)
        {
            //aranan kod süper satır. isimleri karşılaştırıp id yi ekliyor.
            book.BooksCategoryId = db.BooksCategorys.FirstOrDefault(x => x.Name == book.BookType).Id;
          
            
            //if (book.BookType == "ANAOKULU")
            //{
            //    book.BooksCategoryId = 1;
            //}
            //else if (book.BookType == "İLKOKUL")
            //{
            //    book.BooksCategoryId = 2;
            //}
            //else if (book.BookType == "ORTAOKUL")
            //{
            //    book.BooksCategoryId = 3;
            //}
            //else if (book.BookType == "LİSE")
            //{
            //    book.BooksCategoryId = 4;
            //}
            //else if (book.BookType == "ÖZEL ÖĞRETİM")
            //{
            //    book.BooksCategoryId = 5;
            //}
            ViewBag.KayıtHata = "";
            Book bookControl = new Book();
            bookControl = db.Books.FirstOrDefault(x => x.Code == book.Code);
            if (bookControl!=null)
            {
                //ViewBag.KayıtHata = "Bu Kayıt daha önce eklenmiştir.";
                //return RedirectToAction("Create", "Books",);
                return RedirectToAction("Create", new RouteValueDictionary(
                new { controller = "Books", action = "Create", Id = 1 }));
            }
            else
            {
                if (ModelState.IsValid)
                {


                    db.Books.Add(book);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create");
            }




        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Class,BookType")] Book book)
        {
            if (book.BookType == "ANAOKULU")
            {
                book.BooksCategoryId = 1;
            }
            else if (book.BookType == "İLKOKUL")
            {
                book.BooksCategoryId = 2;
            }
            else if (book.BookType == "ORTAOKUL")
            {
                book.BooksCategoryId = 3;
            }
            else if (book.BookType == "LİSE")
            {
                book.BooksCategoryId = 4;
            }
            else if (book.BookType == "ÖZEL ÖĞRETİM")
            {
                book.BooksCategoryId = 5;
            }

            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
