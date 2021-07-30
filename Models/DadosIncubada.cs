using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Table("DadosIncubadas")]
    public class DadosIncubada
    {
        [Key]
        public int EmpresaID { get; set; }

        [StringLength(50)]
        public string Edital { get; set; }

        public int AnoIncubacao { get; set; }

        public Empresa Empresa { get; set; }
    }
}
