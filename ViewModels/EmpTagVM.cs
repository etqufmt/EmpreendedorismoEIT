using EmpreendedorismoEIT.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class EmpTagVM
    {
        public int TagID { get; set; }
        public string Nome { get; set; }
        public int Cor { get; set; }

        [Range(0, 100)]
        public int Grau { get; set; }


        //Atributos formatados
        public string CorHTML => ColorManager.CorHTML((int)Cor);
    }
}
