using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AuthSystem.ApiModels
    
{
    public class ArticoloApiModel
    {
        public int IdArticolo { get; set; }
    

        public string NomeArticolo { get; set; }
       
        [Required]
        public string Descrizione { get; set; }
    
    }
}
