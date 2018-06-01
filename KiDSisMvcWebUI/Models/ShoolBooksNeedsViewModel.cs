using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class ShoolBooksNeedsViewModel
    {
        //public IEnumerable<Book> Books { get; set; }
        //public IEnumerable<BooksNeed> BooksNeeds { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int BookCount { get; set; }
        public string BookCategory { get; set; }
        public string SchoolsCategory { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public string DemandDate { get; set; }



    }
}