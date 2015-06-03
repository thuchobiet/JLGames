using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

[assembly: OwinStartup(typeof(CaBlog.Startup))]

namespace CaBlog
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //System.Web.Helpers.AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Admin/Login"),
                CookieSecure = CookieSecureOption.SameAsRequest
            });
        }
    }
}
