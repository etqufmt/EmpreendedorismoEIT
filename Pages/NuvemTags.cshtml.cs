using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmpreendedorismoEIT.Pages
{
    public class NuvemTagsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NuvemTagsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<TagCloudVM> ListaTags { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ListaTags = await _context.Tags
                .Include(t => t.EmpresasAssociadas)
                .ThenInclude(et => et.Empresa)
                .Select(t => new TagCloudVM
                {
                    ID = t.ID,
                    Nome = t.Nome,
                    Cor = t.Cor,
                    NumAssociacoes = t.EmpresasAssociadas.Where(et => et.Empresa.Situacao == Situacao.ATIVA).Count(),
                })
                .AsNoTracking()
                .ToListAsync();

            //Remover tags sem associação
            ListaTags.RemoveAll(t => t.NumAssociacoes == 0);
            if (ListaTags.Count == 0)
            {
                return Page();
            }

            return Page();
        }
    }
}
