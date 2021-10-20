using EmpreendedorismoEIT.Resources;
using EmpreendedorismoEIT.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{

    public class TagVM
    {
        public int ID { get; set; }

        [FormatText]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [StringLength(30, MinimumLength = 3, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Cores Cor { get; set; }


        //Atributos formatados
        public int CorInt
        {
            get { return (int)Cor; }
            set 
            {
                if (Enum.IsDefined(typeof(Cores), value))
                {
                    Cor = (Cores)value;
                }
                else
                {
                    Cor = 0x0;
                }
            }
        }

        public string CorHTML => TextManager.CorHTML((int)Cor);
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
