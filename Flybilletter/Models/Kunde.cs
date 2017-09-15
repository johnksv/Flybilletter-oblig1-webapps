using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class Kunde
    {
        public int ID { get; set; }
        public string Etternavn { get; set; }
        public string Fornavn { get; set; }
        public DateTime Fodselsdag { get; set; }
        public string Adresse { get; set; }
        public string Tlf { get; set; }
    }
}