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
            var model = new SokViewModel();
            model.Flyplasser = plasser;
            return View(model);
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