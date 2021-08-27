using System;
using EmpreendedorismoEIT.Models;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class IncubadaDetailsVM : EmpresaDetailsVM
    {
        [Display(Name = "Edital")]
        public string Edital { get; set; }

        [Display(Name = "Entrada na incubação")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:Y}")]
        public DateTime MesEntrada { get; set; }

        [Display(Name = "Saída da incubação")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:Y}")]
        public DateTime MesSaida { get; set; }
    }
}
