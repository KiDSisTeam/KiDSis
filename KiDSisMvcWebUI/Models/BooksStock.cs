using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class BooksStock
    {
        public int Id { get; set; }
        //Count : Sayı Adet
        public int BookCount { get; set; }

   
        public Book Books { get; set; }
      
        public User Users { get; set; }


    }
}