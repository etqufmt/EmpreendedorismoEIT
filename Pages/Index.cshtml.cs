using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpreendedorismoEIT.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Empresa> ListaEmp { get; set; }

        public async Task OnGetAsync()
        {
            ListaEmp = await _context.Empresas
                .Where(e => e.Situacao == Situacao.ATIVA)
                .Select(e =>  new Empresa {
                    ID = e.ID,
                    Nome = e.Nome,
                    Tipo = e.Tipo,
                    Logo = e.Logo,
                })
                .OrderBy(e => e.Nome)
                .AsNoTracking().ToListAsync();
        }
    }
}
