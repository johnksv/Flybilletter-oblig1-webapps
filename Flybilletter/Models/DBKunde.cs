using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{

    public class DBPoststed
    {
        [Key]
        public int Postnr { get; set; }
        public string Poststed { get; set; }

        public virtual List<DBKunde> Kunder { get; set; }
    }

    public class DBKunde
    {
        public int ID { get; set; }

        public string Fornavn { get; set; }

        public string Etternavn { get; set; }

        public DateTime Fodselsdag { get; set; }

        public string Adresse { get; set; }

        public string Tlf { get; set; }

        public DBPoststed Poststed { get; set; }



        public static List<Kunde> hentAlle()
        {
            List<Kunde> kunder = null;
            using (var db = new DB())
            {
                kunder = db.Kunder.Select(kunde => new Kunde()
                {
                    ID = kunde.ID,
                    Fornavn = kunde.Fornavn,
                    Etternavn = kunde.Etternavn,
                    Adresse = kunde.Adresse,
                    Poststed = kunde.Poststed.Poststed
                }).ToList();
            }
            return kunder;
        }

        public static List<DBKunde> leggInn(IEnumerable<Kunde> kunder)
        {
            var dbKunder = new List<DBKunde>(kunder.Count());
            foreach (var kunde in kunder)
            {
                dbKunder.Add(leggInn(kunde));
            }
            return dbKunder;
        }

            public static DBKunde leggInn(Kunde innKunde)
        {
            using (var db = new DB())
            {
                var nyKunde = new DBKunde()
                {
                    Fornavn = innKunde.Fornavn,
                    Etternavn = innKunde.Etternavn,
                    Adresse = innKunde.Adresse,
                };

                var poststed = db.Poststeder.Find(innKunde.Postnummer);

                nyKunde.Poststed = poststed;
                db.Kunder.Add(nyKunde);
                db.SaveChanges();
                return nyKunde;
            }
        }
        public static void endreKunde(Kunde innKunde)
        {
            using (var db = new DB())
            {
                DBKunde dbKunde = db.Kunder.Find(innKunde.ID);
                dbKunde.Fornavn = innKunde.Fornavn;
                dbKunde.Etternavn = innKunde.Etternavn;
                dbKunde.Adresse = innKunde.Adresse;

                var poststed = db.Poststeder.Find(innKunde.Postnummer);
                dbKunde.Poststed = poststed;
                db.SaveChanges();

            }
        }
        public static void slettKunde(Kunde innKunde)
        {
            using (var db = new DB())
            {
                DBKunde slettKunde = db.Kunder.Find(innKunde.ID);
                db.Kunder.Remove(slettKunde);
                db.SaveChanges();
            }
        }
        public Kunde hentKundePaaID(int kundeID)
        {
            using (var db = new DB())
            {
                var dbKunde = db.Kunder.Where(kund => kund.ID == kundeID).FirstOrDefault();
                if (dbKunde == null)
                {
                    return null;
                }

                var kunde = new Kunde()
                {
                    ID = dbKunde.ID,
                    Fornavn = dbKunde.Fornavn,
                    Etternavn = dbKunde.Etternavn,
                    Adresse = dbKunde.Adresse,
                    Postnummer = dbKunde.Poststed.Postnr,
                    Poststed = dbKunde.Poststed.Poststed
                };

                return kunde;
            }

        }
    }

}