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
                .Select(t => new TagCloudVM
                {
                    ID = t.ID,
                    Nome = t.Nome,
                    Cor = t.Cor,
                    Associacoes = t.EmpresasAssociadas.Count,
                })
                .AsNoTracking()
                .ToListAsync();

            //Remover tags sem associa��o
            ListaTags.RemoveAll(t => t.Associacoes == 0);
            if (ListaTags.Count == 0)
            {
                return Page();
            }

            //Calcular pesos
            var maxAssociacoes = ListaTags.OrderByDescending(t => t.Associacoes).FirstOrDefault().Associacoes;
            var minAssociacoes = ListaTags.OrderBy(t => t.Associacoes).FirstOrDefault().Associacoes;
            const int maxPeso = 5; //Amplitude da diferen�a de tamanho entre as tags
            const int minPeso = 1; //N�o pode ser zero
            var peso = (decimal)(maxPeso - minPeso) / (maxAssociacoes - minAssociacoes);
            foreach (var tag in ListaTags)
            {
                tag.Associacoes = (int)Math.Ceiling((tag.Associacoes - minAssociacoes) * peso) + minPeso;
            }

            return Page();
        }
    }
}
