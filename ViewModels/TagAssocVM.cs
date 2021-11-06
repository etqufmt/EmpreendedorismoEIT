using System;
using System.ComponentModel.DataAnnotations;
using EmpreendedorismoEIT.Models;

namespace EmpreendedorismoEIT.ViewModels
{
    public class TagAssocVM
    {
        public int EmpresaID { get; set; }
        public string Nome { get; set; }
        public Situacao Situacao { get; set; }

        [Range(0, 100)]
        public int Grau { get; set; }

        //Atributos formatados
        public string NomeSit => Nome + (Situacao == Situacao.ATIVA ? "" : " (Inativa)");
    }
}
