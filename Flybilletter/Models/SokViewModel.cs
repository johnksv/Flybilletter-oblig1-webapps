using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Range (1, 100)]
        [Display(Name = "Antall Billetter")]
        public int AntallBilletter { get; set; }




    }
}