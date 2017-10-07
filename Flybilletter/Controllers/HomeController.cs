using Flybilletter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Flybilletter.Controllers
{
    public class HomeController : Controller
    {

        private DB db = new DB();

        public ActionResult Index()
        {
            //FOR DEBUGGING. TODO: REMOVE
            var referanser = db.Bestillinger.Select(best => best.Referanse).ToArray();
            ViewBag.referanser = referanser;


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
            //TODO: Slå sammen mellomlanding (vise Bodø-Malpensa), Endre reisetid + pris, Radiobutton på kun én

            bool sammeTilOgFra = innSok.Til.Equals(innSok.Fra);
            var fra = db.Flyplasser.Where(flyplass => flyplass.ID == innSok.Fra).First(); //Hvis du tweaket i HTML-koden fortjener du ikke feilmelding
            var til = db.Flyplasser.Where(flyplass => flyplass.ID == innSok.Til).First();

            List<List<Reise>> reiser = null;

            if (ModelState.IsValid && !sammeTilOgFra && fra != null && til != null)
            {
                reiser = new List<List<Reise>>();
                List<Flygning> fraListe = db.Flygninger.Where(flygning => flygning.Rute.Fra.ID.Equals(fra.ID)).ToList(); //fly som drar fra reiseplass
                List<Flygning> tilListe = db.Flygninger.Where(flygning => flygning.Rute.Til.ID.Equals(til.ID)).ToList(); //fly som ender opp i destinasjon
                List<Reise> turListe = new List<Reise>();
                List<Reise> returListe = new List<Reise>();
                foreach (Flygning fraFly in fraListe)
                {
                    if (fraFly.Rute.Til == til)
                    {
                        if (fraFly.AvgangsTid.Date == innSok.Avreise.Date)
                            turListe.Add(new Reise(fraFly));
                    }
                    else
                    {
                        foreach (Flygning tilFly in tilListe)
                        {
                            if (fraFly.Rute.Til == tilFly.Rute.Fra && fraFly.AvgangsTid.Date == innSok.Avreise.Date &&
                                (tilFly.AvgangsTid - fraFly.AnkomstTid) >= new TimeSpan(1, 0, 0))
                            {
                                turListe.Add(new Reise(fraFly, tilFly));
                                break;
                            }
                        }
                    }
                }


                List<Flygning> returFraListe = db.Flygninger.Where(flygning => flygning.Rute.Fra.ID.Equals(til.ID)).ToList();
                List<Flygning> returTilListe = db.Flygninger.Where(flygning => flygning.Rute.Til.ID.Equals(fra.ID)).ToList();

                foreach (Flygning fraFly in returFraListe)
                {
                    if (fraFly.Rute.Til == fra)
                    {
                        if (fraFly.AvgangsTid.Date == innSok.Retur.Date)
                            returListe.Add(new Reise(fraFly));
                    }
                    else
                    {
                        foreach (Flygning tilFly in returTilListe)
                        {
                            //TODO: Ta høyde for tid
                            if (fraFly.Rute.Til == tilFly.Rute.Fra && fraFly.AvgangsTid.Date == innSok.Retur.Date &&
                                (tilFly.AvgangsTid - fraFly.AnkomstTid) >= new TimeSpan(1, 0, 0))
                            {
                                returListe.Add(new Reise(fraFly, tilFly));
                                break;
                            }
                        }
                    }
                }
                reiser.Add(turListe);
                reiser.Add(returListe);

                Session["turListe"] = turListe;
                Session["returListe"] = returListe;
                Session["antallbilletter"] = innSok.AntallBilletter;

            }

            ViewBag.flyplasser = db.Flyplasser.ToList();
            return PartialView("_Flygninger", reiser);
        }



        [HttpPost]
        public ActionResult ValgtReise(string turIndeks, string returIndeks)
        {

            var turListe = (List<Reise>) Session["turListe"];
            var returListe = (List<Reise>) Session["returListe"];

            int turIndeksInt = int.Parse(turIndeks);
            int returIndeksInt = -1;
            if(returIndeks != null) returIndeksInt = int.Parse(returIndeks);

            if (turIndeksInt < 0 || turIndeksInt >= turListe.Count) RedirectToAction("Index");
            if (returIndeksInt < -1 || returIndeksInt >= returListe.Count) RedirectToAction("Index");


            int antallBilletter = (int) Session["antallbilletter"];
            var kunde = new List<Kunde>();
            for (var i = 0; i < antallBilletter; i++)
            {
                kunde.Add(new Kunde());
            }

            var bestillingsdata = new BestillingViewModel()
            {
                Tur = turListe[turIndeksInt],
                Kunder = kunde
            };

            if (returIndeksInt >= 0) bestillingsdata.Retur = returListe[returIndeksInt];

            

            return View("BestillingDetaljer", bestillingsdata);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Kunde(List<Kunde> Kunder)
        {
            return "ASD";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Betaling(BestillingViewModel kort)
        {
            return "ASD";
        }


        [HttpPost]
        public ActionResult BestillingDetaljer(BestillingViewModel bestillingViewModel)
        {
            var gjeldende = (BestillingViewModel)Session["GjeldendeBestilling"];
            if (ModelState.IsValid)
            {

                //Siden gjeldene referer til det samme som Session["GjeldendeBestilling"] slipper vi å gjøre noe mer
                gjeldende.Kunder = bestillingViewModel.Kunder;
                return RedirectToAction("Kvittering");
            }

            return View(gjeldende);
        }


        [ValidateAntiForgeryToken]
        public ActionResult GenererReferanse()
        {
            //TODO: Generer referanse, lagre i database



            var gjeldende = (BestillingViewModel)Session["GjeldendeBestilling"];

            var list = new List<Flygning>();
            var bestilling = new Bestilling()
            {
                BestillingsTidspunkt = DateTime.Now,
                Flygninger = new List<Flygning>(),
                Passasjerer = gjeldende.Kunder
            };

            do //Lag en unik UUID helt til det ikke finnes i databasen fra før.
            {
                bestilling.Referanse = Guid.NewGuid().ToString().ToUpper().Substring(0, 6);
            } while (db.Bestillinger.Where(best => best.Referanse == bestilling.Referanse).Any());


            //Vi må finne de orginale flygningene i databasen for å unngå exception om "Violation of PRIMARY KEY constraint"
            /*foreach (var flygning in gjeldende.Flygninger)
            {
                var dbFlygning = db.Flygninger.Find(flygning.ID);
                if (dbFlygning == null) return View(); //Det skjedde en feil

                bestilling.Flygninger.Add(dbFlygning);
            } */

            db.Bestillinger.Add(bestilling);

            db.SaveChanges();


            TempData["bestilling"] = bestilling;

            return RedirectToAction("Kvittering");
        }

        public ActionResult Kvittering()
        {
            var bestilling = (Bestilling)TempData["bestilling"];
            return View(bestilling);
        }

        [HttpGet]
        public ActionResult ReferanseSok(string referanse)
        {
            Bestilling bestilling = null;

            referanse = referanse.ToUpper().Trim();
            var regex = new Regex("^[A-Z0-9]{6}$");
            bool isMatch = regex.IsMatch(referanse);

            if (isMatch)
            {
                bestilling = db.Bestillinger.Where(best => best.Referanse == referanse).First();
            }

            return View("BestillingInformasjon", bestilling);
        }

        [HttpGet]
        public string ReferanseEksisterer(string referanse)
        {
            string retur = "{{ \"exists\":\"{0}\", \"url\":\"{1}\" }}";
            if (referanse == null) return string.Format(retur, false, null);

            referanse = referanse.ToUpper().Trim();
            var regex = new Regex("^[A-Z0-9]{6}$");
            bool isMatch = regex.IsMatch(referanse);
            bool exists = false;
            string url = "";

            if (isMatch)
            {
                exists = db.Bestillinger.Where(best => best.Referanse == referanse).Any();
                if (exists) url = "/Home/ReferanseSok?referanse=" + referanse;
            }


            return string.Format(retur, exists, url);
        }


        public ActionResult ReferanseSammendrag(string referanse)
        {
            var bestilling = db.Bestillinger.First(best => best.Referanse.Equals(referanse));

            //TODO: Hvis bestilling er null, altså at referansen ikke finnes i databasen.

            return View(bestilling);
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