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

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Incubadas
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
        public IncubadaFormVM IncubadaVM { get; set; }

        public SelectList RamosAtuacaoSL { get; set; }
        public SelectList AnoEntradaSL { get; set; }
        public SelectList AnoSaidaSL { get; set; }

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

            var entradaInc = DateTime.Parse($"{IncubadaVM.AnoEntrada}-{(int)IncubadaVM.MesEntrada}");
            var saidaInc = DateTime.Parse($"{IncubadaVM.AnoSaida}-{(int)IncubadaVM.MesSaida}");
            if (entradaInc.CompareTo(saidaInc) > 0)
            {
                ModelState.AddModelError("IncubadaVM.MesSaida", Resources.ValidationResources.ErrDataAnterior);
                await LoadAsync();
                return Page();
            }

            var novaEmpresa = new Empresa
            {
                Nome = IncubadaVM.Nome,
                Tipo = Tipo.INCUBADA,
                CNPJ = IncubadaVM.CNPJ,
                Segmento = IncubadaVM.Segmento,
                RamoAtuacaoID = IncubadaVM.RamoAtuacaoID,
                Descricao = IncubadaVM.Descricao,
                Endereco = IncubadaVM.Endereco,
                Telefone = IncubadaVM.Telefone,
                Email = IncubadaVM.Email,
                Logo = LogoManager.SalvarImagem(_webHostEnvironment, IncubadaVM.LogoUpload),
                Situacao = IncubadaVM.Situacao,
                UltimaModificacao = DateTime.Now,
                DadosIncubada = new DadosIncubada
                {
                    Edital = IncubadaVM.Edital,
                    MesEntrada = entradaInc,
                    MesSaida = saidaInc,
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
                    ModelState.AddModelError("IncubadaVM.CNPJ", Resources.ValidationResources.ErrCNPJDuplicado);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrCreate);
                }
                await LoadAsync();
                return Page();
            }

            return RedirectToPage("Details", new { novaEmpresa.ID });
        }

        private async Task LoadAsync()
        {
            //Preencher SelectList do ramo de atuação
            if (IncubadaVM?.LogoUpload != null)
            {
                ModelState.AddModelError("IncubadaVM.LogoUpload", Resources.ValidationResources.ErrLogoNovamente);
            } 
            RamosAtuacaoSL = await CacheManager.RamosAtuacaoSL(_memoryCache, _context, _logger);

            //Preencher SelectList dos anos de entrada e saída
            var listaAnoEntrada = Enumerable.Range(2000, (DateTime.Now.Year % 100) + 1).Reverse();
            var listaAnoSaida = Enumerable.Range(2000, (DateTime.Now.Year % 100) + 5).Reverse();
            AnoEntradaSL = new SelectList(listaAnoEntrada, DateTime.Now.Year);
            AnoSaidaSL = new SelectList(listaAnoSaida, DateTime.Now.Year);
        }
    }
}
