using EmpreendedorismoEIT.Models;
using System;

namespace EmpreendedorismoEIT.ViewModels
{
    public class JuniorListaVM
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public Campus Campus { get; set; }
        public string Logo { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime UltimaModificacao { get; set; }
    }
}
