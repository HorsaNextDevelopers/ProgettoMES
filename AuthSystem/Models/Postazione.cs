using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class Postazione
    {

        [Key]
        public int IdPostazione { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Immettere la postazione")]
        [DisplayName("Postazione")]
        public string NomePostazione { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "Immettere la descrizione")]
        [DisplayName("Descrizione")]
        public string Descrizione { get; set; }

    }
}
