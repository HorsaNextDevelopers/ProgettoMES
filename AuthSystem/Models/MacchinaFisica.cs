using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class MacchinaFisica
    {
        [Key]
        [Column(TypeName = "nvarchar(128)")]
        public string CodiceMacchinaFisica { get; set;}
        [Column(TypeName = "nvarchar(250)")]
        public string Descrizione { get; set;}

        public string CodiceCentroDiLavoro { get; set; }
        [ForeignKey("CodiceCentroDiLavoro ")]
        public CentroDiLavoro CentriDiLavoro { get; set; }
    }
}
