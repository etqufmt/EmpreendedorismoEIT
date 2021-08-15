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
        [MaxLength(200)]
        public string CNAE { get; set; }

        public ICollection<Empresa> EmpresasAssociadas { get; set; }
    }
}