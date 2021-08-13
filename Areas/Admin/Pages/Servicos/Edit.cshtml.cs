using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(
            ApplicationDbContext context,
            ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
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

            var prodServ = await _context.ProdServicos
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

            var prodServ = await _context.ProdServicos
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

            try
            {
                _context.Attach(prodServ).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("[DEBUG] ProdutosServicos:update " + ex);
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            return RedirectToPage("Index", new { id = prodServ.EmpresaID });
        }
    }
}
