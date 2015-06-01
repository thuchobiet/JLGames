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
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,ten,ngaytao")] THELOAI theloai)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    var d = from t in db.THELOAIs
                            where t.ten == theloai.ten.Trim()
                            select t;
                    
                    if (d.Count() > 0)
                        throw new Exception("The name was duplicated!");

                    db.THELOAIs.Add(theloai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
        
        public JsonResult GetAll()
        {
            var lst = db.THELOAIs.ToList();
            List<object> result = new List<object>();

            foreach(THELOAI tl in lst)
            {
                string editlink = string.Format("<a title='Chỉnh Sửa' href='/Theloai/Edit/{0}'><i class='fa fa-pencil-square-o'></i></a>", tl.id);
                string detaillink = string.Format("<a title='Xem' href='/Theloai/Details/{0}'><i class='fa fa-file-text'></i></a>", tl.id);                
                string deletellink = string.Format("<a id='{0}' title='Xóa' href='/Theloai/Delete/{0}' onclick='return confirm(' you want to delete?');'><i class='fa fa-times'></i></a>", tl.id);
                //string deletellink = string.Format("<a id='{0}' title='Xóa' href='#'><i class='fa fa-times'></i></a>", tl.id);
                string action = string.Format("{0} | {1} | {2}",editlink,detaillink,deletellink);
                
                result.Add(new { ten  = tl.ten, ngaytao = tl.ngaytao.ToString("dd-MM-yyyy HH:mm:ss"), action = action});
            }

            return Json(result,JsonRequestBehavior.AllowGet);
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
