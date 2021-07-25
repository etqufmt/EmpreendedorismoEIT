using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class DeleteModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public DeleteModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProdutoServico ProdServVM { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = ProdServVM.ID;
            if (id == 0)
            {
                return NotFound();
            }

            var ProdutoServico = await _context.ProdutosServicos
                           .Include(e => e.Empresa)
                           .AsNoTracking()
                           .FirstOrDefaultAsync(m => m.ID == id);
            if (ProdutoServico == null)
            {
                return NotFound();
            }

            try
            {
                _context.ProdutosServicos.Remove(ProdutoServico);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("./Index", new { id = ProdutoServico.EmpresaID, deleteError = true });
            }

            return RedirectToPage("./Index", new { id = ProdutoServico.EmpresaID});
        }
    }
}
