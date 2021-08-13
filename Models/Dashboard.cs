using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Keyless]
    [NotMapped]
    public class Dashboard
    {
        //Utilizada para consulta a múltiplas tabelas
        public int Juniores { get; set; }
        public int Incubadas { get; set; }
        public int Tags { get; set; }
        public int Servicos { get; set; }
    }
}
