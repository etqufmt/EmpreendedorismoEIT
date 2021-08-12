using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpreendedorismoEIT.Models
{
    [Table("RamosAtuacao")]
    public class RamoAtuacao
    {
        public int ID { get; set; }
        
        [Required]
        [StringLength(4)]
        public string CodigoCNAE { get; set; }

        [Required]
        [StringLength(150)]
        public string Denominacao { get; set; }
    }
}