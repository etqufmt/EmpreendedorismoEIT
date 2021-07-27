using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Table("Tags")]
    public class Tag
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(30)]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(6)]
        public string Cor { get; set; }

        public ICollection<EmpresaTag> EmpresasAssociadas { get; set; }
    }
}
