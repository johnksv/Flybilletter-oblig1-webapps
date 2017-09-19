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

            context.Flyplasser.Add(OSL);
            context.Flyplasser.Add(BOO);
            context.Flyplasser.Add(MXP);
            base.Seed(context);
        }
    }
}