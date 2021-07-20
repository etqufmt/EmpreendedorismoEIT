using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace EmpreendedorismoEIT.ViewModels
{
    public class EmpresaJuniorVM
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(140)]
        [Display(Name = "Descrição curta")]
        public string DescricaoCurta { get; set; }

        [StringLength(400)]
        [Display(Name = "Descrição longa")]
        public string DescricaoLonga { get; set; }

        [StringLength(200)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [StringLength(11, MinimumLength = 10)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("([0-9]+)", ErrorMessage ="Número inválido")]
        public string Telefone { get; set; }

        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Selecione um logotipo para a empresa")]
        [DataType(DataType.Upload)]
        [MaxFileSize(2 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [Display(Name = "Logotipo")]
        public IFormFile Logo { get; set; }

        [Required]
        public Situacao Situacao { get; set; }

        [Required]
        public Campus Campus { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Especifique o instituto ou faculdade")]
        [Display(Name = "Instituto/faculdade")]
        public string Instituto { get; set; }
    }
}
