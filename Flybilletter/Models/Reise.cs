using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class Reise
    { 
        public int ID { get; set; }
        public string Referanse { get; set; } //ID til Reisen
        public List<Kunde> Passasjerer { get; set; } //Passasjerer knyttet til en bestilling
        public List<Tur> Turer { get; set; } //Har mange til mange mellom Tur/Reise, må splitte (kanskje)

    }
}