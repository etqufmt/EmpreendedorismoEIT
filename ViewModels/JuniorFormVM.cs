using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.Resources;
using EmpreendedorismoEIT.Utils;
using System.ComponentModel.DataAnnotations;


namespace EmpreendedorismoEIT.ViewModels
{
    public class JuniorFormVM : EmpresaFormVM
    {
        [Display(Name = "Campus")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Campus Campus { get; set; }


        [Display(Name = "Instituto")]
        [StringLength(50, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [FormatText]
        public string Instituto { get; set; }
    }
}
