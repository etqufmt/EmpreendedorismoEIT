using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    public class Tag
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(30)]
        public string Nome { get; set; }
        
        [Required]
        [StringLength(6, MinimumLength = 6)]
        [RegularExpression(@"[a-fA-F0-9]*$")]
        public string Cor {
            get { return _cor; }
            set { _cor = value.ToUpper(); }
        }

        private string _cor;
    }
}
