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

namespace EmpreendedorismoEIT.Areas.Admin.Pages.EmpTags
{
    public class IndexModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public IndexModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<EmpTagsVM> ListaET { get; set; }
        public string NomeEmpresa { get; set; }
        public string ReturnURL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.TagsAssociadas)
                .ThenInclude(t => t.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (empresa == null)
            {
                return NotFound();
            }

            //Criar lista com todas as tags utilizando as associações que já existem
            var allTags = await _context.Tags.AsNoTracking().ToListAsync();
            NomeEmpresa = empresa.Nome;
            ListaET = new List<EmpTagsVM>();
            foreach (var tag in allTags)
            {
                var et = empresa.TagsAssociadas.FirstOrDefault(et => et.TagID == tag.ID);
                int grau = 0;
                if (et != null)
                {
                    grau = (int) Math.Floor(et.Grau * 100);
                }
                ListaET.Add(new EmpTagsVM
                {
                    TagID = tag.ID,
                    Grau = grau,
                    Tag = tag,
                });
            }

            //Botão voltar e títulos
            _preencherRetorno(empresa);

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.TagsAssociadas)
                .ThenInclude(t => t.Tag)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (empresa == null)
            {
                return NotFound();
            }

            _preencherRetorno(empresa);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            empresa.TagsAssociadas.Clear();
            empresa.TagsAssociadas = new List<EmpresaTag>();
            foreach (var et in ListaET)
            {
                if (et.Grau != 0)
                {
                    empresa.TagsAssociadas.Add(new EmpresaTag
                    {
                        EmpresaID = empresa.ID,
                        TagID = et.TagID,
                        Grau = (et.Grau / 100M)
                    });
                }
            }

            _context.Attach(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            return RedirectToPage(ReturnURL);
        }

        private void _preencherRetorno(Empresa empresa)
        {
            ReturnURL = "/Index";
            if (empresa.Tipo == Tipo.JUNIOR)
            {
                ViewData["Section"] = "Juniores";
                ReturnURL = "/Juniores/Index";
            }
            if (empresa.Tipo == Tipo.INCUBADA)
            {
                ViewData["Section"] = "Incubadas";
                ReturnURL = "/Incubadas/Index";
            }
        }
    }
}
