using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Table("RedesSociais")]
    public class RedeSocial
    {
        public int ID { get; set; }

        [Required]
        public int EmpresaID { get; set; }

        [Required]
        public Plataforma Plataforma { get; set; }

        [Required]
        [MaxLength(300)]
        public string URL { get; set; }

        //Referências para outras entidades
        public Empresa Empresa { get; set; }
    }

    public enum Plataforma
    {
        [Display(Name = "Website")]
        WEBSITE = 1,

        [Display(Name = "Facebook")]
        FACEBOOK = 2,

        [Display(Name = "Instagram")]
        INSTAGRAM = 3,

        [Display(Name = "Whatsapp")]
        WHATSAPP = 4,

        [Display(Name = "Twitter")]
        TWITTER = 5,

        [Display(Name = "LinkedIn")]
        LINKEDIN = 6,
    }
}