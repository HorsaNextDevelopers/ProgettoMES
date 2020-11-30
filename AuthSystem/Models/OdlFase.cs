using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class OdlFase
    {
        [Key]
        public int IdFaseOdl { get; set; }

        
        public int CodiceOdl { get; set; }
        [ForeignKey("CodiceOdl")]
        public Odl Odls { get; set; }

        public FaseODLEnum Fase { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Descrizione { get; set; }

        public string CodiceMacchinaFisica { get; set; }
        [ForeignKey("CodiceMacchinaFisica ")]
        public MacchinaFisica MacchineFisiche { get; set; }

        public float TempoStandard { get; set; }

        public OdlStateEnum Stato { get; set; }

    }
   
}
