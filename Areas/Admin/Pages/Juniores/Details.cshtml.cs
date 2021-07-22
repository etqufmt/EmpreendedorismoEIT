using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class DetailsModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public DetailsModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public JunioresVM EmpresaJuniorVM { get; set; }
        public string Logo { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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

            _context.Entry(EJ).Reference(e => e.Empresa).Load();

            EmpresaJuniorVM = new JunioresVM
            {
                ID = EJ.EmpresaID,
                Nome = EJ.Empresa.Nome,
                DescricaoCurta = EJ.Empresa.DescricaoCurta,
                DescricaoLonga = EJ.Empresa.DescricaoLonga,
                Endereco = EJ.Empresa.Endereco,
                Telefone = EJ.Empresa.Telefone,
                Email = EJ.Empresa.Email,
                Situacao = EJ.Empresa.Situacao,
                Campus = EJ.Campus,
                Instituto = EJ.Instituto,
                UltimaModificacao = EJ.Empresa.UltimaModificacao
            };

            Logo = EJ.Empresa.Logo;

            return Page();
        }
    }
}
