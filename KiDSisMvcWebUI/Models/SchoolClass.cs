using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class SchoolClass
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string CategoryId { get; set; }
        public string Category { get; set; }

    }
}