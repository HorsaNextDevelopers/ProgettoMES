using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.Models
{
    public class DistintaBase
    {
        [Key]
        public int IdDistintaBase { get; set; }


        public string CodiceArticolo { get; set; }
        [ForeignKey("CodiceArticolo")]
        [DisplayName("CodicePadre")]
        public Articolo Articoli { get; set; }

       // public int CodiceFiglio { get; set; }
        [ForeignKey("IdDistintaBase")]
        public DistintaBase DistintaBasi { get; set; }
        public int Quantità { get; set; }
    }
}