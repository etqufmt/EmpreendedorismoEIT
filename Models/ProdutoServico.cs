using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Table("ProdutosServicos")]
    public class ProdutoServico
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nome { get; set; }

        [StringLength(400)]
        public string Descricao { get; set; }

        [Required]
        public int EmpresaID { get; set; }

        [Required]
        public Empresa Empresa { get; set; }
    }
}
