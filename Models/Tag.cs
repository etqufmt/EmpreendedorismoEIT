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
        [MaxLength(30)]
        public string Nome { get; set; }

        [Required]
        public uint Cor { get; set; }

        //Referências para outras entidades
        public ICollection<EmpresaTag> EmpresasAssociadas { get; set; }
    }
}
