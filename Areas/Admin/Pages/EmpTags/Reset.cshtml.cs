using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.EmpTags
{
    public class ResetModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ResetModel> _logger;

        public ResetModel( ApplicationDbContext context, ILogger<ResetModel> logger) {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Empresa Empresa { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = Empresa.ID;
            if (id == 0)
            {
                return NotFound();
            }

            var empReset = await _context.Empresas
                .Include(e => e.TagsAssociadas)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (empReset == null)
            {
                return NotFound();
            }

            empReset.TagsAssociadas.Clear();
            empReset.UltimaModificacao = DateTime.Now;

            try
            {
                _context.Attach(empReset).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "[DEBUG] EmpresaTags: Erro ao executar update");
                StatusMessage = Resources.ValidationResources.ErrUpdate;
            }

            return RedirectToPage("Index", new { id = empReset.ID });
        }
    }
}
