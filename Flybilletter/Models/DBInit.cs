using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class DBInit : DropCreateDatabaseAlways<DB>
    {
        protected override void Seed(DB context)
        {
            var OSL = new Flyplass()
            {
                ID = "OSL",
                By = "Oslo",
                Land = "Norge",
                Navn="Gardermoen Lufthavn"

            };
            var BOO = new Flyplass()
            {
                ID = "BOO",
                By = "Bodø",
                Land = "Norge",
                Navn = "Bodø Lufthavn"

            };
            var MXP = new Flyplass()
            {
                ID = "MXP",
                By = "Milano",
                Land = "Italia",
                Navn = "Malpensa lufthavn"

            };

            var BOOOSL = new Rute()
            {
                Fra = BOO,
                Til = OSL,
                BasePris = 1199
            };
            var OSLBOO = new Rute()
            {
                Fra = OSL,
                Til = BOO,
                BasePris = 1199
            };
            var OSLMXP = new Rute()
            {
                Fra = OSL,
                Til = MXP,
                BasePris = 1499
            };
            var MXPOSL = new Rute()
            {
                Fra = MXP,
                Til = OSL,
                BasePris = 1499
            };

            var Ola = new Kunde()
            {
                Etternavn = "Nordmann",
                Fornavn = "Ola",
                Fodselsdag = new DateTime(1970, 1, 18),
                Adresse = "Osloveien 1",
                Tlf = "40000000"
            };
            var Kari = new Kunde()
            {
                Etternavn = "Nordmann",
                Fornavn = "Kari",
                Fodselsdag = new DateTime(1970, 4, 20),
                Adresse = "Osloveien 1",
                Tlf = "40000001"
            };
            var Junior = new Kunde()
            {
                Etternavn = "Nordmann",
                Fornavn = "Ola Junior",
                Fodselsdag = new DateTime(1997, 12, 1),
                Adresse = "Osloveien 1",
                Tlf = "40000002"
            };

            var Boeing737_1 = new Fly()
            {
                Modell = "Boeing 737",
                Sete = new bool[25, 6]
            };
            var Boeing737_2 = new Fly()
            {
                Modell = "Boeing 737",
                Sete = new bool[25, 6]
            };
            var Boeing737_3 = new Fly()
            {
                Modell = "Boeing 737",
                Sete = new bool[25, 6]
            };
            var AirbusA380_1 = new Fly()
            {
                Modell = "Airbus A380",
                Sete = new bool[50, 9] //50 rader, 9 seter hver rad
            };
            var AirbusA380_2 = new Fly()
            {
                Modell = "Airbus A380",
                Sete = new bool[50, 9] //50 rader, 9 seter hver rad
            };


            for (int i = 0; i < 336; i += 4) // 336 timer = 2 uker
            {
                var flygningBOOOSL = new Flygning()
                {
                    AvgangsTid = DateTime.Today.AddHours(i),
                    AnkomstTid = DateTime.Today.AddHours(i + 1),
                    Fly = Boeing737_1,
                    Rute = BOOOSL
                };

                var flygningOSLBOO = new Flygning()
                {
                    AvgangsTid = DateTime.Today.AddHours(i + 2),
                    AnkomstTid = DateTime.Today.AddHours(i + 3),
                    Fly = flygningBOOOSL.Fly,
                    Rute = OSLBOO
                };

                var flygningMXPOSL = new Flygning()
                {
                    AvgangsTid = DateTime.Today.AddHours(i + 0.5),
                    AnkomstTid = DateTime.Today.AddHours(i + 2.5),
                    Fly = AirbusA380_1,
                    Rute = MXPOSL
                };

                var flygningOSLMXP = new Flygning()
                {
                    AvgangsTid = DateTime.Today.AddHours(i + 4),
                    AnkomstTid = DateTime.Today.AddHours(i + 6),
                    Fly = flygningMXPOSL.Fly,
                    Rute = OSLMXP
                };

                context.Flygninger.Add(flygningBOOOSL);
                context.Flygninger.Add(flygningOSLBOO);
                context.Flygninger.Add(flygningMXPOSL);
                context.Flygninger.Add(flygningOSLMXP);
            }

            context.Flyplasser.Add(OSL);
            context.Flyplasser.Add(BOO);
            context.Flyplasser.Add(MXP);
            context.Ruter.Add(BOOOSL);
            context.Ruter.Add(OSLBOO);
            context.Ruter.Add(OSLMXP);
            context.Ruter.Add(MXPOSL);
            context.Kunder.Add(Ola);
            context.Kunder.Add(Kari);
            context.Kunder.Add(Junior);
            context.Fly.Add(Boeing737_1);
            context.Fly.Add(Boeing737_2);
            context.Fly.Add(Boeing737_3);
            context.Fly.Add(AirbusA380_1);
            context.Fly.Add(AirbusA380_2);
            base.Seed(context);
        }
    }
}