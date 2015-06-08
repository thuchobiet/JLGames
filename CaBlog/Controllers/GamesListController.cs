using CaBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CaBlog.Controllers
{
    public class GamesListController : Controller
    {
        private JLBLOGEntities db = new JLBLOGEntities();

        //
        // GET: /GamesList/
        public ActionResult Index()
        {
            var lsGames = db.GAMEs.OrderByDescending(g=>g.ngaytao).ToList();

            return View(lsGames);
        }

        // GET: /GamesList/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GAME game = db.GAMEs.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

	}
}