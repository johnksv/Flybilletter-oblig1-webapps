using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class Reise
    {
        public List<Flygning> Flygninger { get; set; }
        public int Mellomlanding
        {
            get
            {
                return Flygninger.Count - 1;
            }
            private set { }
        }
        public Flyplass Fra
        {
            get
            {
                return Flygninger.First().Rute.Fra;
            }
            private set { }
        }
        public Flyplass Til
        {
            get
            {
                return Flygninger.Last().Rute.Til;
            }
            private set { }
        }
        public double Pris
        {
            get
            {
                return Flygninger.Select(flygning => flygning.Rute.BasePris).Aggregate((pris1, pris2) => pris1 + pris2);
            }
            private set { }
        }
        public DateTime Avgang
        {
            get
            {
                return Flygninger.First().AvgangsTid;
            }
            private set { }
        }
        public DateTime Ankomst
        {
            get
            {
                return Flygninger.Last().AnkomstTid;
            }
            private set { }
        }

        public Reise(Flygning flygning1, params Flygning[] flygninger)
        {
            Flygninger = new List<Flygning>
            {
                flygning1
            };
            Flygninger.AddRange(flygninger);
        }
    }
}