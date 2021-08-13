using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(
            ApplicationDbContext context,
            ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public ProdServico ProdServVM { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet()
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

            var prodServ = await _context.ProdServicos
                .Include(e => e.Empresa)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (prodServ == null)
            {
                return NotFound();
            }

            try
            {
                _context.ProdServicos.Remove(prodServ);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("[DEBUG] ProdutosServicos:delete " + ex);
                StatusMessage = Resources.ValidationResources.ErrDelete;
                return RedirectToPage("Index", new { id = prodServ.EmpresaID});
            }

            return RedirectToPage("Index", new { id = prodServ.EmpresaID});
        }
    }
}
