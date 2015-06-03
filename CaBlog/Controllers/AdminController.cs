using CaBlog.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Web;
using System.Collections.Generic;


namespace CaBlog.Controllers
{

    [Authorize]
    public class AdminController : Controller
    {
        private JLBLOGEntities db = new JLBLOGEntities();
        
        public ActionResult Index()
        {
            ViewBag.slGame = db.GAMEs.Count();
            ViewBag.slTheloai = db.THELOAIs.Count();
            ViewBag.slAccount = db.ACCOUNTs.Count();
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(string username, string password, string returnUrl)
        {
            object result;

            //find user
            var user = from u in db.ACCOUNTs
                       where u.username == username
                       select u;

            if (user.Count() > 0 && user.Single().dangsudung)
            {
                string hashpass = user.Single().passwordhash;
                string salt = user.Single().saltvalue;

                if (ValidatePassword(password, hashpass, salt))
                {
                    HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                    Claim claim1 = new Claim(ClaimTypes.Name, username);
                    Claim[] claims = new Claim[] { claim1 };
                    ClaimsIdentity claimsIdentity =
                      new ClaimsIdentity(claims,
                        DefaultAuthenticationTypes.ApplicationCookie);

                    HttpContext.GetOwinContext().Authentication
                     .SignIn(new AuthenticationProperties() { IsPersistent = false }, claimsIdentity);

                    if (!string.IsNullOrEmpty(returnUrl))
                        result = new { success = "true", link = returnUrl };
                    else
                        result = new { success = "true", link = Url.Content("~/Admin") };
                }
                else
                    result = new { success = "false", link = "" }; 
            }
            else
                result = new { success = "false", link = ""};

            return Json(result);
        }

        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect("/");
        }


        public Boolean ValidatePassword(String enteredPassword, String storedHash, String storedSalt)
        {
            // Consider this function as an internal function where parameters like
            // storedHash and storedSalt are read from the database and then passed.

            var hash = HashPassword(enteredPassword, storedSalt);
            return String.Equals(storedHash, hash);
        }

        public String HashPassword(String password, String salt)
        {
            var combinedPassword = String.Concat(password, salt);
            var sha256 = new SHA256Managed();
            var bytes = UTF8Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
       
	}
}