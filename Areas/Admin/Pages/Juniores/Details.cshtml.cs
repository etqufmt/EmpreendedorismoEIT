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

        public JunioresVM JuniorVM { get; set; }
        public string Logo { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? deleteError = false)
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

            if (deleteError.GetValueOrDefault())
            {
                ErrorMessage = Resources.ValidationResources.ErrDelete;
            }

            JuniorVM = new JunioresVM
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
