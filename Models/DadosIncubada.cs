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

        [MaxLength(70)]
        public string Edital { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime MesEntrada { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime MesSaida { get; set; }

        //Referências para outras entidades
        public Empresa Empresa { get; set; }
    }
}
