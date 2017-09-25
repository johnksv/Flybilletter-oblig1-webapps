﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class Bestilling
    { 
        public int ID { get; set; }
        public string Referanse { get; set; } //ID til bestillingen
        public virtual List<Kunde> Passasjerer { get; set; } //Passasjerer knyttet til en bestilling
        public virtual List<Flygning> Flygninger { get; set; } 
        public DateTime BestillingsTidspunkt { get; set; }

    }
}