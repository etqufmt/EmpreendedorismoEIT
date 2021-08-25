using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.Extensions.Logging;
using System;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(
            ApplicationDbContext context,
            ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public ProdServVM ProdServVM { get; set; }
        public Empresa Empresa { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresas.FindAsync(id);
            if (Empresa == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresas.FindAsync(id);

            if (Empresa == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var prodServ = new ProdServico
            {
                EmpresaID = Empresa.ID,
                Nome = ProdServVM.Nome,
                Descricao = ProdServVM.Descricao
            };
            Empresa.UltimaModificacao = DateTime.Now;

            try
            {
                _context.ProdServicos.Add(prodServ);
                _context.Attach(Empresa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("[DEBUG] ProdutosServicos:create " + ex);
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrCreate);
                return Page();
            }

            return RedirectToPage("Index", new { id });
        }
    }
}
