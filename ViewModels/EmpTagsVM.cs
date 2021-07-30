using EmpreendedorismoEIT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.ViewModels
{
    public class EmpTagsVM
    {
        public int TagID { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }

        [Range(0, 100)]
        public int Grau { get; set; }
    }
}
