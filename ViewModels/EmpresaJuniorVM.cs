using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.Resources;
using EmpreendedorismoEIT.Utils;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace EmpreendedorismoEIT.ViewModels
{
    public class EmpresaJuniorVM
    {
        public int ID { get; set; }

            [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
            [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Nome { get; set; }

            [Display(Name = "Descrição curta")]
            [StringLength(140, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string DescricaoCurta { get; set; }

            [Display(Name = "Descrição longa")]
            [StringLength(400, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string DescricaoLonga { get; set; }

            [Display(Name = "Endereço")]
            [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Endereco { get; set; }

            [DataType(DataType.PhoneNumber)]
            [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Numero", ErrorMessageResourceType = typeof(ValidationResources))]
            [StringLength(11, MinimumLength = 10, ErrorMessageResourceName = "Telefone", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Telefone { get; set; }

            [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(ValidationResources))]
            [StringLength(100, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
            [EmailAddress(ErrorMessageResourceName = "Email", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Email { get; set; }

            [Display(Name = "Logotipo")]
            [DataType(DataType.Upload)]
            [MaxFileSize(2 * 1024 * 1024)]
            [AllowedExtensions(new string[] { ".jpg", ".png" })]
            //[Required(ErrorMessageResourceName = "Logotipo", ErrorMessageResourceType = typeof(ValidationResources))]
        public IFormFile Logo { get; set; }

            [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Situacao Situacao { get; set; }

            [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Campus Campus { get; set; }

            [Display(Name = "Instituto/faculdade")]
            [StringLength(50, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
            [Required(ErrorMessageResourceName = "Instituto", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Instituto { get; set; }
    }
}
