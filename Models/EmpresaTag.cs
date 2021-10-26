using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Column(TypeName = "decimal(5,4)")]
        public double Grau { get; set; }

        //Referências para outras entidades
        public Empresa Empresa { get; set; }
        public Tag Tag { get; set; }
    }
}
