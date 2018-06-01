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
        public string Name { get; set; }
        public string DemandDate { get; set; }
        public int BookId { get; set; }

        public string UserId { get; set; }


    }
}