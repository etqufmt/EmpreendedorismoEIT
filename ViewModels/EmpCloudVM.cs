using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.Resources;
using EmpreendedorismoEIT.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmpreendedorismoEIT.ViewModels
{

    public class EmpCloudVM
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public Tipo Tipo { get; set; }

        public string Descricao { get; set; }

        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string LogoURL { get; set; }

        public Campus JunCampus { get; set; }

        public DateTime IncMesEntrada { get; set; }

        public int PorcentagemMatch { get; set; }

        public ICollection<RedeSocial> RedesSociais { get; set; }
    }
}
