using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmpreendedorismoEIT.Pages
{
    public class IncubadoraModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IncubadoraModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DadosIncubada> ListaEmp { get; set; }

        public async Task OnGet()
        {
            ListaEmp = await _context.DadosIncubadas
                .Include(d => d.Empresa)
                .Where(d => d.Empresa.Situacao == Situacao.ATIVA)
                .OrderBy(d => d.Empresa.Nome).AsNoTracking().ToListAsync();
        }
    }
}
