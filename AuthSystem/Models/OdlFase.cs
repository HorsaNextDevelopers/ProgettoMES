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

        [Column(TypeName = "nvarchar(128)")]
        public string CodiceOdl { get; set; }
        [ForeignKey("CodiceOdl")]
        public Odl Odls { get; set; }
        public string CodiceArticolo { get; set; }
        [ForeignKey("CodiceArticolo ")]
        public Articolo Articoli { get; set; }
        public int FaseOdl { get; set; }
        public string CodiceCentroDiLavoro { get; set; }
        [ForeignKey("CodiceCentroDiLavoro ")]
        public CentroDiLavoro CentriDiLavoro { get; set; }

        public OdlStateEnum Stato { get; set; }

        public string CodiceMacchinaFisica { get; set; }
        [ForeignKey("CodiceMacchinaFisica ")]
        public MacchinaFisica MacchineFisiche { get; set; }

    }
   
}
