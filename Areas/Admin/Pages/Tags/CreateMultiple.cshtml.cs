using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Tags
{
    public class CreateMultiple : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateMultiple> _logger;

        public CreateMultiple(
            ApplicationDbContext context,
            ILogger<CreateMultiple> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public List<TagVM> ListaTagVM { get; set; }
        public int Quantidade { get; set; }

        public IActionResult OnGetAsync()
        {
            Load(null);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? n)
        {
            if (n != null)
            {
                ModelState.Clear();
                Load(n);
                return Page();
            }

            if (!ModelState.IsValid)
            {
                Load(n);
                return Page();
            }

            var listaTag = new List<Tag>();
            foreach (var TagVM in ListaTagVM)
            {
                listaTag.Add(new Tag {
                    Nome = TagVM.Nome,
                    Cor = TagVM.CorInt,
                });
            }

            try
            {
                _context.Tags.AddRange(listaTag);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "[DEBUG] Tags: Erro ao executar create");
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrCreate);
                Load(n);
                return Page();
            }

            return RedirectToPage("Index");
        }

        public void Load(int? n)
        {
            //Quantidade padrão
            Quantidade = n ?? 3;
            Quantidade = Quantidade < 1 ? 1 : Quantidade;
            Quantidade = Quantidade > 10 ? 10 : Quantidade;
            if (ListaTagVM == null)
            {
                return;
            }
            //Adicionar ou remover itens quando houver alteração
            var listaCount = ListaTagVM.Count;
            if (listaCount < Quantidade)
            {
                ListaTagVM.AddRange(new TagVM[Quantidade-listaCount]);
            }
            if (listaCount > Quantidade)
            {
                ListaTagVM.RemoveRange(Quantidade, listaCount - Quantidade);
            }
        }
    }
}
