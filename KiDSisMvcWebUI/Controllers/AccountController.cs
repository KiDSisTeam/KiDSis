using KiDSisMvcWebUI.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using UserIdentity.Models;

namespace KiDSisMvcWebUI.Controllers
{
    /*[Authorize] *///üye girişi gerktirir.
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        //private object userManager;

        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new IdentityDataContext());
            userManager = new UserManager<ApplicationUser>(userStore);
            //parola koşulları kontrol ediliyor.
            userManager.PasswordValidator = new CustomPasswordValidator()
            {
                //parola bir sayısal değer içermek zorunda
                // RequireDigit = true,
                //parola minimum 7 karakter olamk zorunda
                RequiredLength=7,
                //küçük harf içermeli
                //RequireLowercase=true,
                //büyük harf içrtmeli
               // RequireUppercase=true,
                //Alfanumerik değer içermeli
                // RequireNonLetterOrDigit=true,



            };
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {//bir emaili bir kişi kullanabilir.
                RequireUniqueEmail=true,
                //alfa numerik karakter içermesin
                AllowOnlyAlphanumericUserNames=false,
            };
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Erişim hakkınız yok"});
            }
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.Username, model.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "Yanlış kullanıcı adı veya parola");
                }
                else
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };

                    authManager.SignOut();
                    authManager.SignIn(authProperties, identity);

                    return Redirect(string.IsNullOrEmpty(returnUrl) ? "/BooksNeeds/Index" : returnUrl);
                }
            }

            ViewBag.returnUrl = returnUrl;
            return View(model);
        }
        [AllowAnonymous] //üye girişi olmadan ulaşmayı sağlar
        public ActionResult Register()
        {
            return View();
        }
        //// GET: Account
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.UserName = model.UserName;
                user.Email = model.Email;
                var result = userManager.Create(user, model.Password);


                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "User");
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);

                    }
                }

            }
            
            return View(model);
        }
   
        public ActionResult Logout()
        {

            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();

            return RedirectToAction("Login", new RouteValueDictionary(
    new { controller = "Account", action = "Login"}));
            // return RedirectToAction("Login");
        }


    }
}




