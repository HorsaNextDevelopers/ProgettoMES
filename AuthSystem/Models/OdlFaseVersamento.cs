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
        public string IdVersamento { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Immettere la data aggiornata")]
        [DisplayName("Data")]
        public DateTime Data { get; set; }
       
        public string PezziBuoni { get; set; }

        public string PezziDifettosi { get; set; }

        public float TempoEffetivo { get; set; }

        public int IdFase { get; set; }
        [ForeignKey("IdFase")]
        public OdlFase Fasi { get; set; }
     

    }
}
