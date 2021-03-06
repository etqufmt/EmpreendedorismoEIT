using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ProdServVM> ListaProdServ { get; set; }
        public ProdServVM ProdServVM { get; set; }
        public Empresa Empresa { get; set; }
        public string ReturnURL { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresas
                .Include(e => e.ProdServicos)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (Empresa == null)
            {
                return NotFound();
            }

            ListaProdServ = Empresa.ProdServicos.Select(ps => new ProdServVM
            {
                ID = ps.ID,
                EmpresaID = ps.EmpresaID,
                Nome = ps.Nome,
                Descricao = ps.Descricao
            }).OrderBy(ps => ps.Nome).ToList();

            return Page();
        }
    }
}
