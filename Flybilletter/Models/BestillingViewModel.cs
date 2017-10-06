using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class BestillingViewModel
    {

        public Reise Tur { get; set; }

        public Reise Retur { get; set; }

        public KredittkortViewModel Kredittkort { get; set; }

        [Required]
        public List<Kunde> Kunder { get; set; }

        public int AntallBilletter { get; set; }
    }
}