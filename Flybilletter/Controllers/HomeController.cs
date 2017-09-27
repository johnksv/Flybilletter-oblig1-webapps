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


        public ActionResult Bestille()
        {

            var fly = db.Flygninger.Where(f => f.AvgangsTid > DateTime.Now).First();
            var kunder = new List<Kunde>() { new Kunde() };

            var model = new BestillingViewModel()
            {
                Flygninger = new List<Flygning>() { fly },
                Fra = fly.Rute.Fra.By,
                Til = fly.Rute.Til.By,
                Kunder = kunder

            };
            //Lagrer gjeldende bestilling i session slik at vi har denne tilgjengelig vet eventuelle feilmeldinger og endringer.
            Session["GjeldendeBestilling"] = model;

            return View(model);
        }


        [HttpPost]
        public ActionResult Bestille(BestillingViewModel bestillingViewModel)
        {
            var gjeldende = (BestillingViewModel) Session["GjeldendeBestilling"];
            if (ModelState.IsValid)
            {

                //Siden gjeldene referer til det samme som Session["GjeldendeBestilling"] slipper vi å gjøre noe mer
                gjeldende.Kunder = bestillingViewModel.Kunder;
                return RedirectToAction("BestillingBekreftelse");
            }

            return View(gjeldende);
        }

        public ActionResult BestillingBekreftelse()
        {

            var gjeldende = (BestillingViewModel)Session["GjeldendeBestilling"];
            return View(gjeldende);
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