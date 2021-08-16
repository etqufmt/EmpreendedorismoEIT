using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.Resources;
using EmpreendedorismoEIT.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;


namespace EmpreendedorismoEIT.ViewModels
{
    public class JuniorFormVM
    {
        public int ID { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [FormatText]
        public string Nome { get; set; }


        [Display(Name = "CNPJ")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Numero", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(14, MinimumLength = 14, ErrorMessageResourceName = "ErrTelefone", ErrorMessageResourceType = typeof(ValidationResources))]
        public string CNPJ { get; set; }


        [Display(Name = "Segmento")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Segmento Segmento { get; set; }


        [Display(Name = "Ramo de atuação")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public int? RamoAtuacaoID { get; set; }

        [Display(Name = "Descrição da empresa")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(500, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [FormatText]
        public string Descricao { get; set; }


        [Display(Name = "Endereço")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [FormatText]
        public string Endereco { get; set; }


        [Display(Name = "Telefone")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Numero", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(11, MinimumLength = 10, ErrorMessageResourceName = "ErrTelefone", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Telefone { get; set; }


        [Display(Name = "Email")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessageResourceName = "ErrEmail", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(100, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [EmailAddress(ErrorMessageResourceName = "ErrEmail", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Email { get; set; }


        [Display(Name = "Logotipo")]
        [DataType(DataType.Upload)]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile LogoUpload { get; set; }


        [Display(Name = "Situação")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Situacao Situacao { get; set; }


        [Display(Name = "Modificado")]
        [DataType(DataType.Date)]
        public DateTime UltimaModificacao { get; set; }


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
