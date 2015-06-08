using CaBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CaBlog.Controllers
{
    public class HomeController : Controller
    {

        private JLBLOGEntities db = new JLBLOGEntities();

        public ActionResult Index()
        {
            CustomModel resultModel = new CustomModel();

             Random r = new Random();
            //Get randowm danhngon
            var lsDanhNgon = db.DANHNGONs.ToList().OrderBy(x=>r.Next()).Take(1);


            //Get 8 new game order by created date
            var lsGames = (from g in db.GAMEs
                           orderby g.ngaytao descending
                           select g).Take(8);

            //Get 3 new quehuong collection
            var lsQuehuongs = (from g in db.QUEHUONGs
                               orderby g.ngaytao descending
                               select g).Take(3);

            resultModel.lsGame.AddRange(lsGames);
            resultModel.lsQueHuong.AddRange(lsQuehuongs);
            resultModel.lsDanhNgon.Add(lsDanhNgon.FirstOrDefault());

            return View(resultModel);
        }

        public ActionResult QueHuong()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
               

    }
}