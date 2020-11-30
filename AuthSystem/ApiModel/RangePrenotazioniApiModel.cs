using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.ApiModel
{
    public class RangePrenotazioniApiModel
    {
        public DateTime Data { get; set; }
        public string NomePostazione { get; set; }
        public bool Occupato { get; set; }
    }
}
