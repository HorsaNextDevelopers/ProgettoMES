using AuthSystem.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class OdlFaseVersamento
    {
        [Key]
        
        public int IdVersamento { get; set; }

        public string CodiceArticolo { get; set; }
        [ForeignKey("CodiceArticolo ")]
        public Articolo Articoli { get; set; }
        public string CodiceOdl { get; set; }
        [ForeignKey("CodiceOdl ")]
        public Odl Odl { get; set; }
        public int IdFaseODL { get; set; }
        [ForeignKey("IdFaseODL ")]
        public OdlFase OdlFasi { get; set; }

        public string CodiceMacchinaFisica { get; set; }
        [ForeignKey("CodiceMacchinaFisica ")]
        [DisplayName("CodiceMacchinaFisica")]
        public MacchinaFisica MacchinaFisiche { get; set; }
        public string IdAspNetUsers { get; set; }
        [ForeignKey("IdAspNetUsers ")]
        [DisplayName("UserName")]
        public ApplicationUser AspNetUsers { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Immettere la data aggiornata")]
        [DisplayName("Data inizio")]
        public DateTime DataInizio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Immettere la data aggiornata")]
        [DisplayName("Data fine")]
        public DateTime DataFine { get; set; }

        [DataType(DataType.Time)]
        //[DisplayFormat(time= "{0:hh\\-mm\\-ss}", ApplyFormatInEditMode = true)]
        [DisplayName("Tempo di lavoro netto")]
        public DateTime TempoLavoroNetto { get; set; }

        public int PezziBuoni { get; set; }
        public int PezziScartati { get; set; }

    }
}
