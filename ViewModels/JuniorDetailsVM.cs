using EmpreendedorismoEIT.Models;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{
    public class JuniorDetailsVM : EmpresaDetailsVM
    {
        [Display(Name = "Campus")]
        public Campus Campus { get; set; }


        [Display(Name = "Instituto")]
        public string Instituto { get; set; }
    }
}
