using KiDSisMvcWebUI.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KiDSisMvcWebUI.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        //private object userManager;

        public AdminController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);

        }
        // GET: Admin

//eğer kullanıcılar görsün istiyorsak bunu
//[AllowAnonymous] ile açabiliriz.


        //illa kullanıcı girişi yapılarak açılabilir.
       // [Authorize]
        
        public ActionResult Index()
        {
            return View(userManager.Users);
        }
    }
}