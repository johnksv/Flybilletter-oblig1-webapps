using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class Tur
    {
        public List<Reise> Reiser { get; set; }
        public Rute Rute { get; set; }
        public Fly Fly { get; set; }
        public DateTime AvgangsTid { get; set; }
        public DateTime AnkomstTid { get; set; }
        
    }
}