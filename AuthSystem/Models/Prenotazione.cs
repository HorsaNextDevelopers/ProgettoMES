using AuthSystem.Areas.Identity.Data;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Models
{
    public class Prenotazione
    {

        [Key]
        public int IdPrenotazione { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Immettere la data aggiornata")]
        [DisplayName("Data della prenotazione")]
        public DateTime Data { get; set; }

        [MaxLength(450), ForeignKey("AspNetUsers"), DisplayName("Email")]
        public string IdAspNetUsers { get; set; }
        [DisplayName("Email")]
        public ApplicationUser AspNetUsers { get; set; }

        public int IdPostazione { get; set; }
        [ForeignKey("IdPostazione ")]
        [DisplayName("Postazione")]
        public Postazione Postazioni { get; set; }


    }
}
