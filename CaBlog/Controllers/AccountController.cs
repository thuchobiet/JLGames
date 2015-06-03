using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaBlog.Models;
using System.Security.Cryptography;
using System.Text;

namespace CaBlog.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private JLBLOGEntities db = new JLBLOGEntities();

        // GET: /Account/
        public ActionResult Index()
        {
            return View(db.ACCOUNTs.ToList());
        }

        // GET: /Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT account = db.ACCOUNTs.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: /Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Account/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,username,passwordhash,saltvalue,ngaytao,dangsudung")] ACCOUNT account)
        {
            if (ModelState.IsValid)
            {
                account.saltvalue = GetRandomSalt();
                account.passwordhash = HashPassword(account.passwordhash, account.saltvalue);
                db.ACCOUNTs.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        // GET: /Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT account = db.ACCOUNTs.Find(id);

            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: /Account/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,passwordhash,saltvalue,ngaytao,dangsudung")] ACCOUNT account, string password)
        {
            if (ModelState.IsValid)
            {
                account.username = account.username.Trim();
                if (!string.IsNullOrEmpty(password))
                {
                    account.saltvalue = GetRandomSalt();
                    account.passwordhash = HashPassword(password, account.saltvalue);
                }
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: /Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ACCOUNT account = db.ACCOUNTs.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: /Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ACCOUNT account = db.ACCOUNTs.Find(id);
            db.ACCOUNTs.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public String GetRandomSalt(Int32 size = 12)
        {
            var random = new RNGCryptoServiceProvider();
            var salt = new Byte[size];
            random.GetBytes(salt);
            return Convert.ToBase64String(salt);
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
