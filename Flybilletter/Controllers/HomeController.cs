using Flybilletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flybilletter.Controllers
{
    public class HomeController : Controller
    {

        private DB db = new DB();

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Sok()
        {
            List<Flyplass> plasser = db.Flyplasser.ToList();
            var model = new SokViewModel()
            {
                Flyplasser = plasser,
                Avreise = DateTime.Now.Date,
                Retur = DateTime.Now.Date.AddDays(1)
                
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Sok(Flybilletter.Models.SokViewModel innSok)
        {
            if (ModelState.IsValid)
            {

                Response.Write("SØK ER OK");

                return RedirectToAction("Index");

            }


            return View();
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