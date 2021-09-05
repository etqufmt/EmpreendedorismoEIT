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
    public class EmpresaModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EmpresaModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Empresa Emp { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Emp = await _context.Empresas
                .Where(e => e.Situacao == Situacao.ATIVA)
                .Include(d => d.DadosJunior)
                .Include(d => d.DadosIncubada)
                .Include(d => d.RedesSociais)
                .Include(d => d.ProdServicos)
                .Include(d => d.TagsAssociadas)
                .ThenInclude(t => t.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ID == id);

            if (Emp == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
