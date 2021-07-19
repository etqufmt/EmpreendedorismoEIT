using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    public enum Plataforma
    {
        FACEBOOK, INSTAGRAM, WHATSAPP, TWITTER, WEBSITE
    }

    [Table("RedesSociais")]
    public class RedeSocial
    {
        public int ID { get; set; }
        
        [Required]
        public Plataforma Plataforma { get; set; }

        [Required]
        [StringLength(200)]
        public string URL { get; set; }

        [Required]
        public int EmpresaID { get; set; }

        [Required]
        public Empresa Empresa { get; set; }
    }
}