using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Table("ProdServicos")]
    public class ProdServico
    {
        public int ID { get; set; }

        [Required]
        public int EmpresaID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(500)]
        public string Descricao { get; set; }

        //Referências para outras entidades
        public Empresa Empresa { get; set; }
    }
}
