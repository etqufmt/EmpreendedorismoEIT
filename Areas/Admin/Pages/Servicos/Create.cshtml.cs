using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class CreateModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public CreateModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EmpresaID"] = new SelectList(_context.Empresas, "ID", "Nome");
            return Page();
        }

        [BindProperty]
        public ProdutoServico ProdutoServico { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ProdutosServicos.Add(ProdutoServico);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
