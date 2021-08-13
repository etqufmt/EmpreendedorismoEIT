using System;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class EmpTagVM
    {
        public int TagID { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }

        [Range(0, 100)]
        public int Grau { get; set; }
    }
}
