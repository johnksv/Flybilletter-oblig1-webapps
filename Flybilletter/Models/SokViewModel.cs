using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class SokViewModel
    {
        //TODO: Legge til barnebilletter
        public List<Flyplass> Flyplasser { get; set; }
        public Flyplass Fra { get; set; }
        public Flyplass Til { get; set; }
        public DateTime Avreise { get; set; }
        public DateTime Retur { get; set; }
        public int AntallBilletter { get; set; }




    }
}