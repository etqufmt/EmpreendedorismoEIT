using EmpreendedorismoEIT.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.ViewModels
{
    public class ServicosVM
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int EmpresaID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Nome {
            get { return _nome; }
            set { _nome = value?.Trim();  }
        }
        private string _nome;

        [Display(Name = "Descrição")]
        [DisplayFormat(NullDisplayText = "[Não informado]")]
        [StringLength(400, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value?.Trim(); }
        }
        private string _descricao;
    }
}
