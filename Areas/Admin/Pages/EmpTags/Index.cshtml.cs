using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.EmpTags
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            ApplicationDbContext context,
            ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public IList<EmpTagVM> ListaET { get; set; }
        public Empresa Empresa { get; set; }
        public string ReturnURL { get; set; }
        public int Contador { get; set; }
        public int PassoMessage { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? passo)
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

            //Lista todas as tags cadastradas com ou sem associação
            Contador = 0;
            ListaET = new List<EmpTagVM>();
            var allTags = await _context.Tags
                .Include(t => t.EmpresasAssociadas.Where(e => e.EmpresaID == Empresa.ID))
                .OrderBy(t => t.Nome)
                .AsNoTracking()
                .ToListAsync();
            foreach (var tag in allTags)
            {
                int grau = 0;
                var empTag = tag.EmpresasAssociadas.FirstOrDefault();
                if (empTag != null)
                {
                    grau = (int)(empTag.Grau * 100);
                    Contador++;
                }
                ListaET.Add(new EmpTagVM
                {
                    TagID = tag.ID,
                    Nome = tag.Nome,
                    Cor = tag.Cor,
                    Grau = grau,
                });
            }

            PassoMessage = passo ?? 0;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int? passo)
        {
            if (id == null)
            {
                return NotFound();
            }

            PassoMessage = passo ?? 0;
            Empresa = await _context.Empresas
                .Include(e => e.TagsAssociadas)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Empresa == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            //Cadastrar apenas associações com algum grau definido
            var empTags = ListaET.Where(et => et.Grau != 0).ToList();
            if (empTags.Count < 3 && empTags.Count != 0)
            {
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrMinTags);
                return Page();
            }

            Empresa.TagsAssociadas.Clear();
            Empresa.TagsAssociadas = new List<EmpresaTag>();
            foreach (var et in empTags)
            {
                Empresa.TagsAssociadas.Add(new EmpresaTag
                {
                    EmpresaID = Empresa.ID,
                    TagID = et.TagID,
                    Grau = et.Grau / 100.0
                });
            }
            Empresa.UltimaModificacao = DateTime.Now;

            try
            {
                _context.Attach(Empresa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "[DEBUG] EmpresaTags: Erro ao executar update");
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            if (Empresa.Tipo == Tipo.JUNIOR)
            {
                ReturnURL = "/Juniores/Details";
            }
            if (Empresa.Tipo == Tipo.INCUBADA)
            {
                ReturnURL = "/Incubadas/Details";
            }
            if (PassoMessage == 5)
            {
                return RedirectToPage(ReturnURL, new { id = id, passo = 6 });
            }
            return RedirectToPage(ReturnURL, new { id });
        }
    }
}
