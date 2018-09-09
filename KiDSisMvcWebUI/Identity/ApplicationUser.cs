using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string SchoolType { get; set; }
        public string SecondarySchool { get; set; }
        public string PrimarySchool { get; set; }
        public string PreSchool { get; set; }
        public string SpecialEducation { get; set; }
    }
}