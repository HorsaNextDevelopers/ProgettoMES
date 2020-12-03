using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class Odl
    {
        [Key]  
        public int CodiceOdl { get; set; }
        public int QuantitaDaProdurre { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Immettere la data aggiornata")]
        public DateTime DataInizio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Immettere la data aggiornata")]
        public DateTime DataFine { get; set; }
        public string CodiceArticolo { get; set; }
        [ForeignKey("CodiceArticolo ")]
        public Articolo Articoli { get; set; }
        public string CodiceCentroDiLavoro { get; set; }
        [ForeignKey("CodiceCentroDiLavoro ")]
        public CentroDiLavoro CentriDiLavoro { get; set; }
        public OdlStateEnum Stato { get; set; }
    }
   
}
