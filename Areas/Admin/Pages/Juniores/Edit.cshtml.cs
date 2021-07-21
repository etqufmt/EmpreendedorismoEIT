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
        public EmpresaJuniorVM EmpresaJuniorVM { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Carregar tudo de uma vez
            //var EJ = await _context.DadosJuniores.Include(d => d.Empresa)
            //            .AsNoTracking()
            //            .FirstOrDefaultAsync(d => d.ID == id);

            var EJ = await _context.DadosJuniores.FindAsync(id);

            if (EJ == null)
            {
                return NotFound();
            }

            _context.Entry(EJ).Reference(e => e.Empresa).Load();

            EmpresaJuniorVM = new EmpresaJuniorVM
            {
                ID = EJ.ID,
                Nome = EJ.Empresa.Nome,
                DescricaoCurta = EJ.Empresa.DescricaoCurta,
                DescricaoLonga = EJ.Empresa.DescricaoLonga,
                Endereco = EJ.Empresa.Endereco,
                Telefone = EJ.Empresa.Telefone,
                Email = EJ.Empresa.Email,
                Logo = null,
                Situacao = EJ.Empresa.Situacao,
                Campus = EJ.Campus,
                Instituto = EJ.Instituto
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EJ = await _context.DadosJuniores.FindAsync(id);

            if (EJ == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Entry(EJ).Reference(e => e.Empresa).Load();

            EJ.ID = EmpresaJuniorVM.ID;
            EJ.Empresa.Nome = EmpresaJuniorVM.Nome;
            EJ.Empresa.DescricaoCurta = EmpresaJuniorVM.DescricaoCurta;
            EJ.Empresa.DescricaoLonga = EmpresaJuniorVM.DescricaoLonga;
            EJ.Empresa.Endereco = EmpresaJuniorVM.Endereco;
            EJ.Empresa.Telefone = EmpresaJuniorVM.Telefone;
            EJ.Empresa.Email = EmpresaJuniorVM.Email;
            EJ.Empresa.Situacao = EmpresaJuniorVM.Situacao;
            EJ.Campus = EmpresaJuniorVM.Campus;
            EJ.Instituto = EmpresaJuniorVM.Instituto;

            if (EmpresaJuniorVM.Logo != null)
            {
                LogoManager.ExcluirImagem(_webHostEnvironment, EJ.Empresa.Logo);
                EJ.Empresa.Logo = LogoManager.SalvarImagem(_webHostEnvironment, EmpresaJuniorVM.Logo);
            }

            _context.Attach(EJ.Empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
