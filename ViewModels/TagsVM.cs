using EmpreendedorismoEIT.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.ViewModels
{

    public class TagsVM
    {
        public int ID { get; set; }

        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(20, MinimumLength = 2, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Nome
        {
            get { return _nome; }
            set { _nome = value?.Substring(0, 1).ToUpper() + value?.Substring(1); }
        }
        private string _nome;

        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Cores Cor { get; set; }
    }

    public enum Cores
    {
        [Display(Name = "Amarelo")]
        AMARELO = 0xFECE67,

        [Display(Name = "Oliva")]
        OLIVA = 0x999966,

        [Display(Name = "Azul")]
        AZUL = 0x75C0E0,

        [Display(Name = "Lilás")]
        LILAS = 0xB676B1,

        [Display(Name = "Vermelho")]
        VERMELHO = 0xEE778F,

        [Display(Name = "Verde")]
        VERDE = 0x42BD88,

        [Display(Name = "Salmão")]
        SALMAO = 0xFFAA80,

        [Display(Name = "Roxo")]
        ROXO = 0x9999FF,

        [Display(Name = "Verde claro")]
        VERDEC = 0x82CAAF,
    }
}
