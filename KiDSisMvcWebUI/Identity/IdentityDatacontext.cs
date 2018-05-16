using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiDSisMvcWebUI.Identity
{
    public class IdentityDataContext:IdentityDbContext<ApplicationUser>
    {// base kısmına var olan bir connection string ismini yazmalmalı
        public IdentityDataContext():base("IdentityConnection")
        {

        }



    }
}