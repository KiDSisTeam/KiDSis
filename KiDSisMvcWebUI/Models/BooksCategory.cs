using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class BooksCategory
    {
        public int Id { get; set; }
        public String Name { get; set; }

        //Kitaplar tablosundaki kayıtlar buraya eklenmiş oldu.
        public List<Book> Books { get; set; }
    }
}