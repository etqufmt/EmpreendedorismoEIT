using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Models
{
    [Keyless]
    public class DashboardQuery
    {
        public int Juniores { get; set; }
        public int Incubadas { get; set; }
        public int Tags { get; set; }
        public int Servicos { get; set; }
    }
}
