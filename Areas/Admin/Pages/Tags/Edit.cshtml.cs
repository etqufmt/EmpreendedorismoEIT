using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using EmpreendedorismoEIT.Models;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Tags
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
        public TagVM TagVM { get; set; }
        public List<TagAssocVM> ListaAssoc { get; set; }
        public string TagHiddenMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            TagVM = new TagVM
            {
                ID = tag.ID,
                Nome = tag.Nome,
                CorInt = tag.Cor,
            };

            await LoadAsync(tag.ID);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(tag.ID);
                return Page();
            }

            tag.Nome = TagVM.Nome;
            tag.Cor = TagVM.CorInt;

            try
            {
                _context.Attach(tag).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "[DEBUG] Tags: Erro ao executar update");
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            return RedirectToPage("Index");
        }

        private async Task LoadAsync(int id)
        {
            ListaAssoc = await _context.EmpresaTags
                .Include(et => et.Empresa)
                .Where(et => et.TagID == id)
                .Select(et => new TagAssocVM
                {
                    EmpresaID = et.EmpresaID,
                    Nome = et.Empresa.Nome,
                    Situacao = et.Empresa.Situacao,
                    Grau = (int)(et.Grau * 100),
                })
                .ToListAsync();

            if(!ListaAssoc.Where(et => et.Situacao == Situacao.ATIVA).Any())
            {
                TagHiddenMessage = Resources.ValidationResources.ErrTagHiddenCloud;
            }
        }
    }
}
