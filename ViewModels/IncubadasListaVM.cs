using EmpreendedorismoEIT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.ViewModels
{
    public class IncubadasListaVM
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int AnoIncubacao { get; set; }
        public string Logo { get; set; }
        public Situacao Situacao { get; set; }
    }
}
