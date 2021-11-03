using EmpreendedorismoEIT.Resources;
using EmpreendedorismoEIT.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{

    public class TagCloudVM
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public int Cor { get; set; }

        public int NumAssociacoes { get; set; }

        //Atributos formatados
        public string CorHTML => TextManager.CorHTML(Cor);
    }
}
