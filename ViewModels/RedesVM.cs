using EmpreendedorismoEIT.Resources;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class RedesVM
    {
        public int EmpresaID { get; set; }

        [Display(Name = "Site próprio")]
        [RegularExpression(@"^https:\/\/(.*)", ErrorMessageResourceName = "ErrURL", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string WebsiteURL { get; set; }

        [Display(Name = "Facebook")]
        [RegularExpression(@"^(?:https:\/\/)(?:[^.]+\.)?facebook\.com(\/.*)?$", ErrorMessageResourceName = "ErrSite", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string FacebookURL { get; set; }

        [Display(Name = "Instagram")]
        [RegularExpression(@"^(?:https:\/\/)(?:[^.]+\.)?instagram\.com(\/.*)?$", ErrorMessageResourceName = "ErrSite", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string InstagramURL { get; set; }

        [Display(Name = "WhatsApp")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Numero", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceName = "ErrTelefone", ErrorMessageResourceType = typeof(ValidationResources))]
        public string WhatsappNUM { get; set; }

        [Display(Name = "Twitter")]
        [RegularExpression(@"^(?:https:\/\/)(?:[^.]+\.)?twitter\.com(\/.*)?$", ErrorMessageResourceName = "ErrSite", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string TwitterURL { get; set; }

        [Display(Name = "LinkedIn")]
        [RegularExpression(@"^(?:https:\/\/)(?:[^.]+\.)?linkedin\.com(\/.*)?$", ErrorMessageResourceName = "ErrSite", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string LinkedinURL { get; set; }

        public int Contagem { get; set; }

        //Atributos formatados
        public string WhatsappURL
        {
            get 
            {
                //Código internacional para telefones brasileiros = 55
                const string wppAPI = "https://api.whatsapp.com/send?phone=55";
                return wppAPI + WhatsappNUM;
            }
        }
    }
}
