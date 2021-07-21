using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class IndexModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public IndexModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<DadosJunior> ListaEJ { get;set; }

        public async Task OnGetAsync()
        {
            ListaEJ = await _context.DadosJuniores.Include(dj => dj.Empresa).AsNoTracking().ToListAsync();
        }
    }
}
