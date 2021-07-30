using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.AspNetCore.Hosting;
using EmpreendedorismoEIT.ViewModels;
using EmpreendedorismoEIT.Utils;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Incubadas
{
    public class EditModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EditModel(EmpreendedorismoEIT.Data.ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public IncubadasVM IncubadaVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Carregar tudo de uma vez
            var EI = await _context.DadosIncubadas
                        .Include(d => d.Empresa)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(d => d.EmpresaID == id);

            if (EI == null)
            {
                return NotFound();
            }

            IncubadaVM = new IncubadasVM
            {
                ID = EI.EmpresaID,
                Edital = EI.Edital,
                AnoIncubacao = EI.AnoIncubacao,
                Nome = EI.Empresa.Nome,
                DescricaoCurta = EI.Empresa.DescricaoCurta,
                DescricaoLonga = EI.Empresa.DescricaoLonga,
                Endereco = EI.Empresa.Endereco,
                Telefone = EI.Empresa.Telefone,
                Email = EI.Empresa.Email,
                Logo = null,
                Situacao = EI.Empresa.Situacao
            };

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
                return Page();
            }

            EI.EmpresaID = IncubadaVM.ID;
            EI.Edital = IncubadaVM.Edital;
            EI.AnoIncubacao = IncubadaVM.AnoIncubacao;
            EI.Empresa.Nome = IncubadaVM.Nome;
            EI.Empresa.DescricaoCurta = IncubadaVM.DescricaoCurta;
            EI.Empresa.DescricaoLonga = IncubadaVM.DescricaoLonga;
            EI.Empresa.Endereco = IncubadaVM.Endereco;
            EI.Empresa.Telefone = IncubadaVM.Telefone;
            EI.Empresa.Email = IncubadaVM.Email;
            EI.Empresa.Situacao = IncubadaVM.Situacao;
            EI.Empresa.UltimaModificacao = DateTime.Now;

            string logoAntigo = null;
            if (IncubadaVM.Logo != null)
            {
                logoAntigo = EI.Empresa.Logo;
                EI.Empresa.Logo = LogoManager.SalvarImagem(_webHostEnvironment, IncubadaVM.Logo);
            }

            _context.Attach(EI).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                LogoManager.ExcluirImagem(_webHostEnvironment, logoAntigo);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            return RedirectToPage("Details", new { id });
        }
    }
}
