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
        [Key]
        public int EmpresaID { get; set; }

        [Required]
        public Campus Campus { get; set; }

        [Required]
        [MaxLength(50)]
        public string Instituto { get; set; }

        //Referências para outras entidades
        public Empresa Empresa { get; set; }
    }

    public enum Campus
    {
        [Display(Name = "Cuiabá")]
        CUIABA = 1,

        [Display(Name = "Araguaia")]
        ARAGUAIA = 2,

        [Display(Name = "Sinop")]
        SINOP = 3,
    }

}
