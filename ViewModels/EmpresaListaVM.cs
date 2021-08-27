using System;
using EmpreendedorismoEIT.Models;

namespace EmpreendedorismoEIT.ViewModels
{
    public class EmpresaListaVM
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Logo { get; set; }
        public Situacao Situacao { get; set; }
        public DateTime UltimaModificacao { get; set; }
    }
}
