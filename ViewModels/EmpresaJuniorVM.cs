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

        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Nome { get; set; }


        [Display(Name = "Descrição curta")]
        [StringLength(140, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string DescricaoCurta { get; set; }


        [Display(Name = "Descrição longa")]
        [StringLength(500, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string DescricaoLonga { get; set; }


        [Display(Name = "Endereço")]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Endereco { get; set; }


        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Numero", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(11, MinimumLength = 10, ErrorMessageResourceName = "ErrTelefone", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Telefone { get; set; }


        [Display(Name = "Email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessageResourceName = "ErrEmail", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(100, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [EmailAddress(ErrorMessageResourceName = "ErrEmail", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Email { get; set; }


        [Display(Name = "Logotipo")]
        [DataType(DataType.Upload)]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        //[Required(ErrorMessageResourceName = "ErrLogotipo", ErrorMessageResourceType = typeof(ValidationResources))]
        public IFormFile Logo { get; set; }


        [Display(Name = "Situação")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Situacao Situacao { get; set; }


        [Display(Name = "Campus")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Campus Campus { get; set; }


        [Display(Name = "Instituto/faculdade")]
        [StringLength(50, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [Required(ErrorMessageResourceName = "ErrInstituto", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Instituto { get; set; }
    }
}
