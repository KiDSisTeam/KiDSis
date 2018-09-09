using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Models
{
    public class User
    {
        public int Id { get; set; }
        // Institution: kurum
        public int InstitutionCode { get; set; }
        public string InstitutionName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string EPosta { get; set; }
        public string Role { get; set; }
        public int SchoolsCategoryId { get; set; }
        public string PrimarySchool { get; set; }
        public string PreSchool { get; set; }
        public string SpecialEducation { get; set; }
        public string SecondarySchool { get; set; }





    }
}