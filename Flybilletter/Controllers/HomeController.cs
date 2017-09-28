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
            ViewBag.flyplasser = db.Flyplasser.ToList();

            var model = new SokViewModel()
            {
                Avreise = DateTime.Now.Date,
                Retur = DateTime.Now.Date.AddDays(1)

            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Sok(Flybilletter.Models.SokViewModel innSok)
        {
            //TODO: Mulig vi må gjøre om på denne; Er dette når man har valgt flygning og trykker "Neste"?

            bool sammeTilOgFra = innSok.Til.Equals(innSok.Fra);
            bool fra = db.Flyplasser.Where(flyplass => flyplass.ID == innSok.Fra).Any(); //Hvis du tweaket i HTML-koden fortjener du ikke feilmelding
            bool til = db.Flyplasser.Where(flyplass => flyplass.ID == innSok.Til).Any();


            if (ModelState.IsValid && !sammeTilOgFra && fra && til)
            {
                return RedirectToAction("Index");
            }

            ViewBag.flyplasser = db.Flyplasser.ToList();
            return View(innSok);
        }


        public ActionResult BestillingDetaljer()
        {

            var fly = db.Flygninger.Include("Fly").Where(f => f.AvgangsTid > DateTime.Now).First();
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
        public ActionResult BestillingDetaljer(BestillingViewModel bestillingViewModel)
        {
            var gjeldende = (BestillingViewModel)Session["GjeldendeBestilling"];
            if (ModelState.IsValid)
            {

                //Siden gjeldene referer til det samme som Session["GjeldendeBestilling"] slipper vi å gjøre noe mer
                gjeldende.Kunder = bestillingViewModel.Kunder;
                return RedirectToAction("BestillingOppsummering");
            }

            return View(gjeldende);
        }

        public ActionResult BestillingOppsummering()
        {
            var gjeldende = (BestillingViewModel)Session["GjeldendeBestilling"];
            return View(gjeldende);
        }

        [HttpPost]
        public ActionResult BestillingOppsummering(BestillingViewModel innModel)
        {
            //TODO: Generer referanse, lagre i database
            return RedirectToAction("Kvittering");
        }

        public ActionResult Kvittering()
        {
            //TODO: Send inn bestillingen som har blitt generert
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