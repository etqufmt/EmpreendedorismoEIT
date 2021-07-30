using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class EditModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public EditModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServicosVM ProdServVM { get; set; }
        public Empresa Empresa { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodServ = await _context.ProdutosServicos
                .Include(e => e.Empresa)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (prodServ == null)
            {
                return NotFound();
            }

            Empresa = prodServ.Empresa;
            ProdServVM = new ServicosVM
            {
                ID = prodServ.ID,
                Nome = prodServ.Nome,
                Descricao = prodServ.Descricao,
                EmpresaID = prodServ.EmpresaID
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodServ = await _context.ProdutosServicos
                .Include(e => e.Empresa)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (prodServ == null)
            {
                return NotFound();
            }

            Empresa = prodServ.Empresa;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            prodServ.Nome = ProdServVM.Nome;
            prodServ.Descricao = ProdServVM.Descricao;

            _context.Attach(prodServ).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            return RedirectToPage("./Index", new { id = prodServ.EmpresaID });
        }
    }
}
