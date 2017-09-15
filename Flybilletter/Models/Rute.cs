using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class Rute
    {
        public int ID { get; set; }
        public Flyplass Fra { get; set; }
        public Flyplass Til { get; set; }
        public DateTime AvgangsTid { get; set; }
        public DateTime AnkomstTid { get; set; }
       


    }
}