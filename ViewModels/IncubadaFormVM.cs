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

        [Display(Name = "Entrada na incubação")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Meses MesEntrada { get; set; }

        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [Range(2000, 2100, ErrorMessageResourceName = "ErrAno", ErrorMessageResourceType = typeof(ValidationResources))]
        public int AnoEntrada { get; set; }

        [Display(Name = "Saída da incubação")]
        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        public Meses MesSaida { get; set; }

        [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
        [Range(2000, 2100, ErrorMessageResourceName = "ErrAno", ErrorMessageResourceType = typeof(ValidationResources))]
        public int AnoSaida { get; set; }
    }

    public enum Meses
    {
        [Display(Name = "Janeiro")]
        JAN = 1,

        [Display(Name = "Fevereiro")]
        FEV = 2,

        [Display(Name = "Março")]
        MAR = 3,

        [Display(Name = "Abril")]
        ABR = 4,

        [Display(Name = "Maio")]
        MAI = 5,

        [Display(Name = "Junho")]
        JUN = 6,

        [Display(Name = "Julho")]
        JUL = 7,

        [Display(Name = "Agosto")]
        AGO = 8,

        [Display(Name = "Setembro")]
        SET = 9,

        [Display(Name = "Outubro")]
        OUT = 10,

        [Display(Name = "Novembro")]
        NOV = 11,

        [Display(Name = "Dezembro")]
        DEZ = 12,
    }
}
