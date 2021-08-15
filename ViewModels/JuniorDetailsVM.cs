using EmpreendedorismoEIT.Models;
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


        //Atributos formatados
        public string CNPJFormatado { get
            {
                string res = null;
                if (!String.IsNullOrEmpty(CNPJ))
                {
                    try
                    {
                        res = Int64.Parse(CNPJ).ToString(@"##\.###\.###/####-##");
                    }
                    catch { }
                }
                return res;
            }
        }

        public string TelefoneFormatado { get
            {
                string res = null;
                if (!String.IsNullOrEmpty(Telefone))
                {
                    try
                    {
                        if (Telefone.Length == 10)
                        {
                            res = Int64.Parse(Telefone).ToString(@"(##) ####-####");
                        }
                        if (Telefone.Length == 11)
                        {
                            res = Int64.Parse(Telefone).ToString(@"(##) #####-####");
                        }
                    }
                    catch { }
                }
                return res;
            }
        }
    }
}
