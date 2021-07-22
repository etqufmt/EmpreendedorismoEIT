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

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
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
        public JunioresVM EmpresaJuniorVM { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? error = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EJ = await _context.DadosJuniores.FindAsync(id);

            if (EJ == null)
            {
                return NotFound();
            }

            if (error.GetValueOrDefault())
            {
                ErrorMessage = Resources.ValidationResources.ErrDelete;
            }

            _context.Entry(EJ).Reference(e => e.Empresa).Load();

            EmpresaJuniorVM = new JunioresVM
            {
                ID = EJ.Empresa.ID,
                Nome = EJ.Empresa.Nome,
                Campus = EJ.Campus,
                DescricaoCurta = EJ.Empresa.DescricaoCurta,
                Situacao = EJ.Empresa.Situacao,
                UltimaModificacao = EJ.Empresa.UltimaModificacao,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            var logoAtual = empresa.Logo;

            if (empresa == null)
            {
                return NotFound();
            }

            try
            {
                _context.Empresas.Remove(empresa);
                await _context.SaveChangesAsync();
                LogoManager.ExcluirImagem(_webHostEnvironment, logoAtual);
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("./Delete", new { id, error = true });
            }

            return RedirectToPage("./Index");
        }
    }
}
