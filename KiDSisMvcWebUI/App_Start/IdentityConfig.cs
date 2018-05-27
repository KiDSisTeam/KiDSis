using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Web.WebPages;

[assembly: OwinStartup(typeof(KiDSisMvcWebUI.IdentityConfig))]

namespace KiDSisMvcWebUI
{
    public class IdentityConfig
    {
        public string DefaultAutohenticationTypes { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {

                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")

            });

            //!Roles.RoleExists("Member") 
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888

        }
    }
}
