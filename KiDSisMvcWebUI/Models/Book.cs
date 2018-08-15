using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        //Kitap türü ders kitabı çalışma kitabı öğretmen kitabı
        public string BookType { get; set; }
        public int BooksCategoryId { get; set; }
        
    }
}