using System;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class IncubadaListaVM : EmpresaListaVM
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMM/yyyy}")]
        public DateTime MesEntrada { get; set; }
    }
}
