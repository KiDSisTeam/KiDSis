using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class BooksDeliveryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookCount { get; set; }
        public int BookId { get; set; }
        public string SchoolsName { get; set; }
        public string SchoolsCategory { get; set; }
        public string Class { get; set; }
        public string Recipient { get; set; }
        public string Deliverer { get; set; }
        public int Year { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }



    }
}