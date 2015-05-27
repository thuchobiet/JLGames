using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaBlog.Models;

namespace CaBlog.Controllers
{
    public class TheloaiController : Controller
    {
        private JLBLOGEntities db = new JLBLOGEntities();

        // GET: /Theloai/
        public ActionResult Index()
        {
            return View(db.THELOAIs.ToList());
        }

        // GET: /Theloai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THELOAI theloai = db.THELOAIs.Find(id);
            if (theloai == null)
            {
                return HttpNotFound();
            }
            return View(theloai);
        }

        // GET: /Theloai/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Theloai/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[ValidateAntiForgeryToken]
        [HttpPost]
        
        public ActionResult Create([Bind(Include="id,ten,ngaytao")] THELOAI theloai)
        {
            theloai.ngaytao = DateTime.Now;

            /*
            if (ModelState.IsValid)
            {
                db.THELOAIs.Add(theloai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            */

            


            if (!string.IsNullOrEmpty(theloai.ten))
            {
                var item = from i in db.THELOAIs
                           where i.ten == theloai.ten
                           select i;
                if (item != null)
                {
                    db.THELOAIs.Add(theloai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(theloai);
        }

        // GET: /Theloai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THELOAI theloai = db.THELOAIs.Find(id);
            if (theloai == null)
            {
                return HttpNotFound();
            }
            return View(theloai);
        }

        // POST: /Theloai/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,ten,ngaytao")] THELOAI theloai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theloai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theloai);
        }

        // GET: /Theloai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THELOAI theloai = db.THELOAIs.Find(id);
            if (theloai == null)
            {
                return HttpNotFound();
            }
            return View(theloai);
        }

        // POST: /Theloai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            THELOAI theloai = db.THELOAIs.Find(id);
            db.THELOAIs.Remove(theloai);
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
    }
}
