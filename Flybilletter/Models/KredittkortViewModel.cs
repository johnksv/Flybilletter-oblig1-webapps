using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flybilletter.Models
{
    public class KredittkortViewModel
    {

        [Required]
        [RegularExpression("^[A-Za-zæøåÆØÅ ]+$", ErrorMessage = "Ugyldig navn.")]
        public string Kortholder { get; set; }

        [Required]
        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Ugyldig kortnummer.")]
        public int Kortnummer { get; set; }

        [Required]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "Ugyldig cvc.")]
        public int CVC{ get; set; }

        [Required]
        [RegularExpression("^[0-9]{2}-[0-9]{2}$", ErrorMessage = "Ugyldig utløpsdato")]
        public string Utlop { get; set; }
    }
}