using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class Fly
    {
        public int ID { get; set; }
        public string Modell { get; set; } //Modell-navn til flyet
        public bool[,] Sete { get; set; } // true er ledig, false er ikke ledig
        public virtual List<Flygning> Flygninger { get; set; }
    }
}