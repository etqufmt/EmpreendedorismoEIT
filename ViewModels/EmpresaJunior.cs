using EmpreendedorismoEIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.ViewModels
{
    public class EmpresaJunior
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public Tipo Tipo { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoLonga { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public Situacao Situacao { get; set; }
        public Campus Campus { get; set; }
        public string Instituto { get; set; }
    }
}
