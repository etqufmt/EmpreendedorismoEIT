using System;
using System.Linq;
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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CreateModel> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(
            ApplicationDbContext context,
            IMemoryCache memoryCache,
            ILogger<CreateModel> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _memoryCache = memoryCache;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public JuniorFormVM JuniorVM { get; set; }

        public SelectList RamosAtuacaoSL { get; set; }

        [TempData]
        public bool JustCreatedMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadAsync();
                return Page();
            }

            var novaEmpresa = new Empresa
            {
                Nome = JuniorVM.Nome,
                Tipo = Tipo.JUNIOR,
                CNPJ = JuniorVM.CNPJ,
                Segmento = JuniorVM.Segmento,
                RamoAtuacaoID = JuniorVM.RamoAtuacaoID,
                Descricao = JuniorVM.Descricao,
                Endereco = JuniorVM.Endereco,
                Telefone = JuniorVM.Telefone,
                Email = JuniorVM.Email,
                Logo = LogoManager.SalvarImagem(_webHostEnvironment, JuniorVM.LogoUpload),
                Situacao = JuniorVM.Situacao,
                UltimaModificacao = DateTime.Now,
                DadosJunior = new DadosJunior
                {
                    Campus = JuniorVM.Campus,
                    Instituto = JuniorVM.Instituto
                },
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
                SqlException innerException = ex.InnerException as SqlException;
                //Checa por erro de campo duplicado (UNIQUE) do Microsoft SQL Server / Azure SQL Server
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    ModelState.AddModelError("JuniorVM.CNPJ", Resources.ValidationResources.ErrCNPJDuplicado);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrCreate);
                }
                await LoadAsync();
                return Page();
            }

            JustCreatedMessage = true;
            return RedirectToPage("Details", new { novaEmpresa.ID });
        }

        private async Task LoadAsync()
        {
            if (JuniorVM?.LogoUpload != null)
            {
                ModelState.AddModelError("JuniorVM.LogoUpload", Resources.ValidationResources.ErrLogoNovamente);
            } 
            RamosAtuacaoSL = await CacheManager.RamosAtuacaoSL(_memoryCache, _context, _logger);
        }
    }
}
