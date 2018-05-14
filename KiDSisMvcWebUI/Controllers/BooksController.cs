﻿using System;
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
        public ActionResult Create()
        {
            List<string> SchoolList = new List<string>
            {"ANAOKULU","İLKOKUL", "ORTAOKUL","LİSE","ÖZEL ÖĞRETİM"};
            ViewBag.ShoolListViewBag = SchoolList;
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Class,BookType,BooksCategoryId")] Book book)
        {
            if (book.BookType=="ANAOKULU")
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


                db.Books.Add(book);              
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
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
