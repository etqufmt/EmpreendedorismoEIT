using EmpreendedorismoEIT.Resources;
using EmpreendedorismoEIT.Utils;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class ProdServVM
    {
        public int ID { get; set; }

        public int EmpresaID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [FormatText]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [DisplayFormat(NullDisplayText = "[Não informada]")]
        [StringLength(500, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [FormatText]
        public string Descricao { get; set; }
    }
}
