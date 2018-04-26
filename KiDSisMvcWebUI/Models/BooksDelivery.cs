using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class BooksDelivery
    {
        public int Id { get; set; }
        public int BookCount { get; set; }

        public Book BookId { get; set; }
        public Book Books { get; set; }
        public User UsersId { get; set; }
        public User Users { get; set; }

    }
}