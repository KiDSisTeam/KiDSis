using KiDSisMvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Entity
{//DbContext işlemleri yapabilmek için DbContext Sınıfından miras alması gerekir.
    public class DataContext:DbContext
    {//eklenen base kodu istenen veritabanını oluşturmaya yardımcı olur.
        //base kısmına veritabanı ismi yazılmalıdır.
        public DataContext() : base("KiDSisDb")
        {
            Database.SetInitializer(new DataInitializer());
        }
        public DbSet<Authority> Authoritys { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BooksCategory> BooksCategorys { get; set; }
        public DbSet<BooksDelivery> BooksDeliverys { get; set; }
        public DbSet<BooksNeed> BooksNeeds { get; set; }
        public DbSet<BooksStock> BooksStocks { get; set; }
        public DbSet<SchoolsCategory> SchoolsCategorys { get; set; }
        public DbSet<User> Users { get; set; }

        public System.Data.Entity.DbSet<KiDSisMvcWebUI.Models.ShoolBooksNeedsViewModel> ShoolBooksNeedsViewModels { get; set; }
    }
}