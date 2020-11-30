using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Models
{
    public class DistintaBase
    {
        [Key]
        public int IdDistintaBase { get; set; }

        [DisplayName("CodicePadre")]
        public string CodiceArticolo { get; set; }
        [ForeignKey("CodiceArticolo")]
        public Articolo ArticoloPadre { get; set; }

        public string CodiceFiglio { get; set; }
        [ForeignKey("CodiceFiglio")]
        public Articolo ArticoloFiglio { get; set; }
        public int Quantità { get; set; }
    }
}