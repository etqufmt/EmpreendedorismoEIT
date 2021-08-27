using System;
using EmpreendedorismoEIT.Resources;
using EmpreendedorismoEIT.Utils;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class IncubadaFormVM : EmpresaFormVM
    {
        [Display(Name = "Edital")]
        [StringLength(50, ErrorMessageResourceName = "Tamanho", ErrorMessageResourceType = typeof(ValidationResources))]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [FormatText]
        public string Edital { get; set; }

        [Display(Name = "Mês de entrada")]
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public DateTime MesEntrada { get; set; }

        [Display(Name = "Mês de saída")]
        [DataType(DataType.Date)]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public DateTime MesSaida { get; set; }
    }
}
