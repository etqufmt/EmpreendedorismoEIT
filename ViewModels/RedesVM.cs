using EmpreendedorismoEIT.Resources;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class RedesVM
    {
        [Required]
        public int EmpresaID { get; set; }

        [Display(Name = "Site próprio")]
        [DataType(DataType.Url, ErrorMessageResourceName = "ErrURL", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string WebsiteURL { get; set; }

        [Display(Name = "Facebook")]
        [RegularExpression(@"^(?:https?:\/\/)?(?:[^.]+\.)?facebook\.com(\/.*)?$", ErrorMessageResourceName = "ErrSite", ErrorMessageResourceType = typeof(ValidationResources))]
        [DataType(DataType.Url, ErrorMessageResourceName = "ErrURL", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string FacebookURL { get; set; }

        [Display(Name = "Instagram")]
        [RegularExpression(@"^(?:https?:\/\/)?(?:[^.]+\.)?instagram\.com(\/.*)?$", ErrorMessageResourceName = "ErrSite", ErrorMessageResourceType = typeof(ValidationResources))]
        [DataType(DataType.Url, ErrorMessageResourceName = "ErrURL", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string InstagramURL { get; set; }

        [Display(Name = "WhatsApp")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Numero", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceName = "ErrTelefone", ErrorMessageResourceType = typeof(ValidationResources))]
        public string WhatsappURL { get; set; }

        [Display(Name = "Twitter")]
        [RegularExpression(@"^(?:https?:\/\/)?(?:[^.]+\.)?twitter\.com(\/.*)?$", ErrorMessageResourceName = "ErrSite", ErrorMessageResourceType = typeof(ValidationResources))]
        [DataType(DataType.Url, ErrorMessageResourceName = "ErrURL", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string TwitterURL { get; set; }
    }
}
