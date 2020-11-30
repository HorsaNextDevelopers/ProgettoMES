using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class Articolo
    {
        [Key]
        public string CodiceArticolo {get; set;}

       
        [Column(TypeName = "nvarchar(250)")]
        public string Descrizione { get; set; }

        public CategoriaArtEnum Categoria { get; set; }

    }
}
