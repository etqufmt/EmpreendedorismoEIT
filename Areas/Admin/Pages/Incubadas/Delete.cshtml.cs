using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.Extensions.Logging;
using EmpreendedorismoEIT.ViewModels;
using EmpreendedorismoEIT.Utils;
using Microsoft.AspNetCore.Hosting;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Incubadas
{
    public class DeleteModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(EmpreendedorismoEIT.Data.ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public IncubadasVM IncubadaVM { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = IncubadaVM.ID;
            if (id == 0)
            {
                return NotFound();
            }

            var EI = await _context.DadosIncubadas
                        .Include(d => d.Empresa)
                        .FirstOrDefaultAsync(d => d.EmpresaID == id);
            if (EI == null)
            {
                return NotFound();
            }

            var logoAtual = EI.Empresa.Logo;
            try
            {
                _context.Empresas.Remove(EI.Empresa);
                await _context.SaveChangesAsync();
                LogoManager.ExcluirImagem(_webHostEnvironment, logoAtual);
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("Details", new { id, deleteError = true });
            }

            return RedirectToPage("Index");
        }
    }
}
