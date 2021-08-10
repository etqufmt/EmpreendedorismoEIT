using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.AspNetCore.Hosting;
using EmpreendedorismoEIT.Utils;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CreateModel> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(
            ApplicationDbContext context,
            ILogger<CreateModel> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
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
            catch (DbUpdateException ex)
            {
                _logger.LogError("[DEBUG] Empresas:create // " + ex);
                LogoManager.ExcluirImagem(_webHostEnvironment, novaEmpresa.Logo);
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrCreate);
                return Page();
            }

            return RedirectToPage("Details", new { novaEmpresa.ID });
        }
    }
}
