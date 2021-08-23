using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class CreateMultiple : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateMultiple(
            ApplicationDbContext context,
            ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public List<ProdServVM> ListaProdServVM { get; set; }
        public Empresa Empresa { get; set; }
        public int Quantidade { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? n)
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

            Quantidade = n ?? 5;
            Quantidade = Quantidade > 10 ? 10 : Quantidade;
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

            List<ProdServico> listaPS = new List<ProdServico>();
            foreach (var ProdServVM in ListaProdServVM)
            {
                listaPS.Add(new ProdServico {
                    EmpresaID = Empresa.ID,
                    Nome = ProdServVM.Nome,
                    Descricao = ProdServVM.Descricao,
                });
            }

            try
            {
                _context.ProdServicos.AddRange(listaPS);
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
