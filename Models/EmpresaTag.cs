using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Table("EmpresaTags")]
    public class EmpresaTag
    {
        [Required]
        public int EmpresaID { get; set; }

        [Required]
        public int TagID { get; set; }

        [Required]
        [Range(0, 1)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Grau { get; set; }

        //Referências para outras entidades
        public Empresa Empresa { get; set; }
        public Tag Tag { get; set; }
    }
}
