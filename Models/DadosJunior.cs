using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Table("DadosJuniores")]
    public class DadosJunior
    {
        [ForeignKey("Empresa")]
        public int ID { get; set; }

        [Required]
        public Campus Campus { get; set; }

        [Required]
        [StringLength(50)]
        public string Instituto { get; set; }

        public Empresa Empresa { get; set; }
    }

    public enum Campus
    {
        [Display(Name = "Cuiabá")]
        CUIABA,

        [Display(Name = "Araguaia")]
        ARAGUAIA,

        [Display(Name = "Sinop")]
        SINOP,

        [Display(Name = "Várzea Grande")]
        VGRANDE
    }

}
