using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class ShoolBooksStocksViewModel
    {
        public int Id { get; set; }
        public string SchoolsCategory { get; set; }
        public string Name { get; set; }
        public string DemandDate { get; set; }
        public string Class { get; set; }
        public int BookCount { get; set; }


    }
}