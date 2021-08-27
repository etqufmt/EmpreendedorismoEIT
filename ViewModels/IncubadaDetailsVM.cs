using System;
using EmpreendedorismoEIT.Models;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class IncubadaDetailsVM : EmpresaDetailsVM
    {
        [Display(Name = "Edital")]
        public string Edital { get; set; }

        [Display(Name = "Mês de entrada")]
        [DataType(DataType.Date)]
        public DateTime MesEntrada { get; set; }

        [Display(Name = "Mês de saída")]
        [DataType(DataType.Date)]
        public DateTime MesSaida { get; set; }
    }
}
