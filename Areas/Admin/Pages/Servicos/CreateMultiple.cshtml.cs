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

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class CreateMultiple : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateMultiple(
            ApplicationDbContext context,
            ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public List<ProdServVM> ListaProdServVM { get; set; }
        public Empresa Empresa { get; set; }
        public int Quantidade { get; set; }
        public string ReturnURL { get; set; }
        public int PassoMessage { get; set; }

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

            PassoMessage = passo ?? 0;
            Load(null);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int? n, int? passo)
        {
            if (id == null)
            {
                return NotFound();
            }

            PassoMessage = passo ?? 0;
            Empresa = await _context.Empresas.FindAsync(id);
            if (Empresa == null)
            {
                return NotFound();
            }

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

            var listaPS = new List<ProdServico>();
            foreach (var ProdServVM in ListaProdServVM)
            {
                listaPS.Add(new ProdServico {
                    EmpresaID = Empresa.ID,
                    Nome = ProdServVM.Nome,
                    Descricao = ProdServVM.Descricao,
                });
            }
            Empresa.UltimaModificacao = DateTime.Now;

            try
            {
                _context.ProdServicos.AddRange(listaPS);
                _context.Attach(Empresa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "[DEBUG] ProdutosServicos: Erro ao executar create");
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrCreate);
                Load(n);
                return Page();
            }

            if(PassoMessage == 3) {
                return RedirectToPage("/Redes/Index", new { id = id, passo = 4 });
            }
            return RedirectToPage("Index", new { id });
        }

        public void Load(int? n)
        {
            //Quantidade padrão
            Quantidade = n ?? 3;
            Quantidade = Quantidade < 1 ? 1 : Quantidade;
            Quantidade = Quantidade > 10 ? 10 : Quantidade;
            if (ListaProdServVM == null)
            {
                return;
            }
            //Adicionar ou remover itens quando houver alteração
            var listaCount = ListaProdServVM.Count;
            if (listaCount < Quantidade)
            {
                ListaProdServVM.AddRange(new ProdServVM[Quantidade-listaCount]);
            }
            if (listaCount > Quantidade)
            {
                ListaProdServVM.RemoveRange(Quantidade, listaCount - Quantidade);
            }
        }
    }
}
