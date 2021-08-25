using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.ViewModels
{
    public class JuniorDetailsVM
    {
        public int ID { get; set; }


        [Display(Name = "Nome")]
        public string Nome { get; set; }


        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; }


        [Display(Name = "Segmento")]
        public Segmento Segmento { get; set; }


        [Display(Name = "Ramo de atuação")]
        public string RamoAtuacaoCNAE { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }


        [Display(Name = "Endereço")]
        public string Endereco { get; set; }


        [Display(Name = "Telefone")]
        public string Telefone { get; set; }


        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }


        [Display(Name = "Logotipo")]
        public string Logo { get; set; }


        [Display(Name = "Situação")]
        public Situacao Situacao { get; set; }


        [Display(Name = "Modificado")]
        [DataType(DataType.Date)]
        public DateTime UltimaModificacao { get; set; }


        [Display(Name = "Campus")]
        public Campus Campus { get; set; }


        [Display(Name = "Instituto")]
        public string Instituto { get; set; }

        public Contagem Contagem { get; set; }


        //Atributos formatados
        public string CNPJFormatado => TextManager.FormatarCNPJ(CNPJ);

        public string TelefoneFormatado => TextManager.FormatarTelefone(Telefone);
    }

    public class Contagem
    {
        public int RedesSociais { get; set; }
        public int ProdServicos { get; set; }
        public int TagsAssociadas { get; set; }
    }
}
