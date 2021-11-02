using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using Microsoft.AspNetCore.Hosting;
using EmpreendedorismoEIT.ViewModels;
using EmpreendedorismoEIT.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Incubadas
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<EditModel> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(
            ApplicationDbContext context,
            IMemoryCache memoryCache,
            ILogger<EditModel> logger,
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

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EI = await _context.DadosIncubadas
                .Include(d => d.Empresa)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.EmpresaID == id);

            if (EI == null)
            {
                return NotFound();
            }

            IncubadaVM = new IncubadaFormVM
            {
                ID = EI.EmpresaID,
                Nome = EI.Empresa.Nome,
                CNPJ = EI.Empresa.CNPJ,
                Segmento = EI.Empresa.Segmento,
                RamoAtuacaoID = EI.Empresa.RamoAtuacaoID,
                Descricao = EI.Empresa.Descricao,
                Endereco = EI.Empresa.Endereco,
                Telefone = EI.Empresa.Telefone,
                Email = EI.Empresa.Email,
                LogoUpload = null,
                Situacao = EI.Empresa.Situacao,
                Edital = EI.Edital,
                MesEntrada = (Meses)EI.MesEntrada.Month,
                AnoEntrada = EI.MesEntrada.Year,
                MesSaida = (Meses)EI.MesSaida.Month,
                AnoSaida = EI.MesSaida.Year,
            };

            await LoadAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EI = await _context.DadosIncubadas
                .Include(d => d.Empresa)
                .FirstOrDefaultAsync(d => d.EmpresaID == id);

            if (EI == null)
            {
                return NotFound();
            }

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

            EI.Empresa.Nome = IncubadaVM.Nome;
            EI.Empresa.Segmento = IncubadaVM.Segmento;
            EI.Empresa.RamoAtuacaoID = IncubadaVM.RamoAtuacaoID;
            EI.Empresa.Descricao = IncubadaVM.Descricao;
            EI.Empresa.Endereco = IncubadaVM.Endereco;
            EI.Empresa.Telefone = IncubadaVM.Telefone;
            EI.Empresa.Email = IncubadaVM.Email;
            EI.Empresa.Situacao = IncubadaVM.Situacao;
            EI.Empresa.UltimaModificacao = DateTime.Now;
            EI.Edital = IncubadaVM.Edital;
            EI.MesEntrada = entradaInc;
            EI.MesSaida = saidaInc;

            string logoAntigo = null;
            string logoAtual = null;
            if (IncubadaVM.LogoUpload != null)
            {
                logoAntigo = EI.Empresa.Logo;
                logoAtual = LogoManager.SalvarImagem(_webHostEnvironment, IncubadaVM.LogoUpload);
                EI.Empresa.Logo = logoAtual;
            }

            try
            {
                _context.Attach(EI).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                LogoManager.ExcluirImagem(_webHostEnvironment, logoAntigo);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "[DEBUG] Incubadas: Erro ao executar update");
                LogoManager.ExcluirImagem(_webHostEnvironment, logoAtual);
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                await LoadAsync();
                return Page();
            }

            return RedirectToPage("Details", new { id });
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
