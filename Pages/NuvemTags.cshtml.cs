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

        public async Task OnGet()
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

            //Calcular pesos
            ListaTags.RemoveAll(t => t.Associacoes == 0);
            var maxAssociacoes = ListaTags.OrderByDescending(t => t.Associacoes).FirstOrDefault().Associacoes;
            var minAssociacoes = ListaTags.OrderBy(t => t.Associacoes).FirstOrDefault().Associacoes;
            const int maxPeso = 15; //Amplitude da diferença de tamanho entre as tags
            const int minPeso = 10; //Não pode ser zero
            var peso = (decimal)(maxPeso - minPeso) / (maxAssociacoes - minAssociacoes);
            foreach (var tag in ListaTags)
            {
                tag.Associacoes = (int)Math.Ceiling((tag.Associacoes - minAssociacoes) * peso) + minPeso;
            }

        }
    }
}
