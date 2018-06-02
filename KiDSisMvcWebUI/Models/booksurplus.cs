using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class Booksurplus
    {
        public int Id { get; set; }
        public int BookCount { get; set; }
        public string Name { get; set; }
        public string DemandDate { get; set; }
        public string SchoolName { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }


    }
}