using System;
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

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
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
        public JuniorFormVM JuniorVM { get; set; }

        public SelectList RamosAtuacaoSL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EJ = await _context.DadosJuniores
                .Include(d => d.Empresa)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.EmpresaID == id);

            if (EJ == null)
            {
                return NotFound();
            }

            JuniorVM = new JuniorFormVM
            {
                ID = EJ.EmpresaID,
                Nome = EJ.Empresa.Nome,
                CNPJ = EJ.Empresa.CNPJ,
                Segmento = EJ.Empresa.Segmento,
                RamoAtuacaoID = EJ.Empresa.RamoAtuacaoID,
                Descricao = EJ.Empresa.Descricao,
                Endereco = EJ.Empresa.Endereco,
                Telefone = EJ.Empresa.Telefone,
                Email = EJ.Empresa.Email,
                LogoUpload = null,
                Situacao = EJ.Empresa.Situacao,
                Campus = EJ.Campus,
                Instituto = EJ.Instituto,
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

            var EJ = await _context.DadosJuniores
                .Include(d => d.Empresa)
                .FirstOrDefaultAsync(d => d.EmpresaID == id);

            if (EJ == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync();
                return Page();
            }

            EJ.Empresa.Nome = JuniorVM.Nome;
            EJ.Empresa.Segmento = JuniorVM.Segmento;
            EJ.Empresa.RamoAtuacaoID = JuniorVM.RamoAtuacaoID;
            EJ.Empresa.Descricao = JuniorVM.Descricao;
            EJ.Empresa.Endereco = JuniorVM.Endereco;
            EJ.Empresa.Telefone = JuniorVM.Telefone;
            EJ.Empresa.Email = JuniorVM.Email;
            EJ.Empresa.Situacao = JuniorVM.Situacao;
            EJ.Empresa.UltimaModificacao = DateTime.Now;
            EJ.Campus = JuniorVM.Campus;
            EJ.Instituto = JuniorVM.Instituto;

            string logoAntigo = null;
            string logoAtual = null;
            if (JuniorVM.LogoUpload != null)
            {
                logoAntigo = EJ.Empresa.Logo;
                logoAtual = LogoManager.SalvarImagem(_webHostEnvironment, JuniorVM.LogoUpload);
                EJ.Empresa.Logo = logoAtual;
            }

            try
            {
                _context.Attach(EJ).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                LogoManager.ExcluirImagem(_webHostEnvironment, logoAntigo);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("[DEBUG] Empresas:update // " + ex);
                LogoManager.ExcluirImagem(_webHostEnvironment, logoAtual);
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                await LoadAsync();
                return Page();
            }

            return RedirectToPage("Details", new { id });
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
