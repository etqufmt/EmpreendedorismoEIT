using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.Resources;
using EmpreendedorismoEIT.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.ViewModels
{
    public class IncubadasVM
    {
        public int ID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Nome { get; set; }


        [Display(Name = "Descrição curta")]
        [DisplayFormat(NullDisplayText = "[Não informado]")]
        [StringLength(140, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string DescricaoCurta { get; set; }


        [Display(Name = "Descrição longa")]
        [DisplayFormat(NullDisplayText = "[Não informado]")]
        [StringLength(500, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string DescricaoLonga { get; set; }


        [Display(Name = "Endereço")]
        [DisplayFormat(NullDisplayText = "[Não informado]")]
        [StringLength(200, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Endereco { get; set; }


        [Display(Name = "Telefone")]
        [DisplayFormat(NullDisplayText = "[Não informado]")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "Numero", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(11, MinimumLength = 10, ErrorMessageResourceName = "ErrTelefone", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Telefone { get; set; }


        [Display(Name = "Email")]
        [DisplayFormat(NullDisplayText = "[Não informado]")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessageResourceName = "ErrEmail", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(100, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [EmailAddress(ErrorMessageResourceName = "ErrEmail", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Email { get; set; }


        [Display(Name = "Logotipo")]
        [DataType(DataType.Upload)]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        //[Required(ErrorMessageResourceName = "ErrLogotipo", ErrorMessageResourceType = typeof(ValidationResources))]
        public IFormFile Logo { get; set; }


        [Display(Name = "Situação")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Situacao Situacao { get; set; }


        [Display(Name = "Modificado")]
        [DataType(DataType.Date)]
        public DateTime UltimaModificacao { get; set; }


        [Display(Name = "Edital")]
        [DisplayFormat(NullDisplayText = "[Não informado]")]
        [StringLength(50, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Edital { get; set; }


        [Display(Name = "Ano de incubação")]
        [Range(1900,2100, ErrorMessageResourceName = "ErrAno", ErrorMessageResourceType = typeof(ValidationResources))]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public int AnoIncubacao { get; set; }
    }
}
