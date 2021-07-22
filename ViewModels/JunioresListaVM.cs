using EmpreendedorismoEIT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.ViewModels
{
    public class JunioresListaVM
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public Campus Campus { get; set; }
        public string Logo { get; set; }
    }
}
