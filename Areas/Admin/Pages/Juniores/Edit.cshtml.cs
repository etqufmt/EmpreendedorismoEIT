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

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
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
        public JunioresVM JuniorVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Carregar tudo de uma vez
            var EJ = await _context.DadosJuniores
                        .Include(d => d.Empresa)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(d => d.EmpresaID == id);

            if (EJ == null)
            {
                return NotFound();
            }

            JuniorVM = new JunioresVM
            {
                ID = EJ.EmpresaID,
                Campus = EJ.Campus,
                Instituto = EJ.Instituto,
                Nome = EJ.Empresa.Nome,
                DescricaoCurta = EJ.Empresa.DescricaoCurta,
                DescricaoLonga = EJ.Empresa.DescricaoLonga,
                Endereco = EJ.Empresa.Endereco,
                Telefone = EJ.Empresa.Telefone,
                Email = EJ.Empresa.Email,
                Logo = null,
                Situacao = EJ.Empresa.Situacao
            };

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
                return Page();
            }

            EJ.EmpresaID = JuniorVM.ID;
            EJ.Campus = JuniorVM.Campus;
            EJ.Instituto = JuniorVM.Instituto;
            EJ.Empresa.Nome = JuniorVM.Nome;
            EJ.Empresa.DescricaoCurta = JuniorVM.DescricaoCurta;
            EJ.Empresa.DescricaoLonga = JuniorVM.DescricaoLonga;
            EJ.Empresa.Endereco = JuniorVM.Endereco;
            EJ.Empresa.Telefone = JuniorVM.Telefone;
            EJ.Empresa.Email = JuniorVM.Email;
            EJ.Empresa.Situacao = JuniorVM.Situacao;
            EJ.Empresa.UltimaModificacao = DateTime.Now;

            string logoAntigo = null;
            if (JuniorVM.Logo != null)
            {
                logoAntigo = EJ.Empresa.Logo;
                EJ.Empresa.Logo = LogoManager.SalvarImagem(_webHostEnvironment, JuniorVM.Logo);
            }

            _context.Attach(EJ).State = EntityState.Modified;

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

            return RedirectToPage("./Details", new { id });
        }
    }
}
