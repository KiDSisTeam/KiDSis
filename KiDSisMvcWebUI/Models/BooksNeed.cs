using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class BooksNeed
    {
        public int Id { get; set; }
        public int BookCount { get; set; }
        public string Name { get; set; }
        public string DemandDate { get; set; }
        public string ShoolName { get; set; }

        public int BookId { get; set; }
              
        public string UserId { get; set; }



    }
}