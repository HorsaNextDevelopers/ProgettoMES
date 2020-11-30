using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class CentroDiLavoro
    {
        [Key]
        [Column(TypeName = "nvarchar(128)")]
        public string CodiceCentroDiLavoro{ get; set; }
        [Column(TypeName = "nvarchar(250)")]
        public string Descrzione { get; set; }
        public ICollection<MacchinaFisica> MacchineFisiche { get; set; }
    }
}
