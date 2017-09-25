using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class BestillingViewModel
    {

        public String Fra { get; set; }

        public String Til { get; set; }

        public IEnumerable<Flygning> Flygninger { get; set; }

        public DateTime Avreise { get; set; }

        public DateTime Retur { get; set; }

        [Required]
        public List<Kunde> Kunder { get; set; }

        public int AntallBilletter
        {
            get
            {
                return Kunder.Count();
            }

            private set { }
        }
    }
}