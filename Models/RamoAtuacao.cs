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
        [MaxLength(10)]
        public string CNAE { get; set; }

        [Required]
        [MaxLength(150)]
        public string Denominacao { get; set; }

        //Referências para outras entidades
        public ICollection<Empresa> EmpresasAssociadas { get; set; }
    }
}