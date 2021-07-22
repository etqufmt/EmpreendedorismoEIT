using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.AspNetCore.Hosting;
using EmpreendedorismoEIT.Utils;
using Microsoft.EntityFrameworkCore;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class CreateModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(EmpreendedorismoEIT.Data.ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public JunioresVM JuniorVM { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var novaEmpresa = new Empresa
            {
                Nome = JuniorVM.Nome,
                Tipo = Tipo.JUNIOR,
                DescricaoCurta = JuniorVM.DescricaoCurta,
                DescricaoLonga = JuniorVM.DescricaoLonga,
                Endereco = JuniorVM.Endereco,
                Telefone = JuniorVM.Telefone,
                Email = JuniorVM.Email,
                Logo = LogoManager.SalvarImagem(_webHostEnvironment, JuniorVM.Logo),
                Situacao = JuniorVM.Situacao,
                DadosJunior = new DadosJunior
                {
                    Campus = JuniorVM.Campus,
                    Instituto = JuniorVM.Instituto
                },
                UltimaModificacao = DateTime.Now
            };

            try
            {
                _context.Empresas.Add(novaEmpresa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrCreate);
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
