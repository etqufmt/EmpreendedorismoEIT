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
        public int EmpresaID { get; set; }
        
        public int TagID { get; set; }

        [Range(0, 1)]
        public decimal Grau { get; set; }

        [Required]
        public Empresa Empresa { get; set; }
        
        [Required]
        public Tag Tag { get; set; }
    }
}
