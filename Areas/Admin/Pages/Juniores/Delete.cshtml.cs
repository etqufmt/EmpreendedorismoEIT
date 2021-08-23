using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using Microsoft.Extensions.Logging;
using EmpreendedorismoEIT.ViewModels;
using EmpreendedorismoEIT.Utils;
using Microsoft.AspNetCore.Hosting;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeleteModel> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(
            ApplicationDbContext context,
            ILogger<DeleteModel> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public JuniorFormVM JuniorVM { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = JuniorVM.ID;
            if (id == 0)
            {
                return NotFound();
            }

            var EJ = await _context.DadosJuniores
                .Include(d => d.Empresa)
                .FirstOrDefaultAsync(d => d.EmpresaID == id);

            if (EJ == null)
            {
                return NotFound();
            }

            var logoAtual = EJ.Empresa.Logo;
            try
            {
                _context.Empresas.Remove(EJ.Empresa);
                await _context.SaveChangesAsync();
                LogoManager.ExcluirImagem(_webHostEnvironment, logoAtual);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("[DEBUG] Empresas:delete // " + ex);
                StatusMessage = Resources.ValidationResources.ErrDelete;
                return RedirectToPage("Details", new { id });
            }

            return RedirectToPage("Index");
        }
    }
}
