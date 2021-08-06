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

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Incubadas
{
    public class DetailsModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public DetailsModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IncubadasVM IncubadaVM { get; set; }
        public string Logo { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? deleteError = false)
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

            if (deleteError.GetValueOrDefault())
            {
                ErrorMessage = Resources.ValidationResources.ErrDelete;
            }

            IncubadaVM = new IncubadasVM
            {
                ID = EI.EmpresaID,
                Nome = EI.Empresa.Nome,
                DescricaoCurta = EI.Empresa.DescricaoCurta,
                DescricaoLonga = EI.Empresa.DescricaoLonga,
                Endereco = EI.Empresa.Endereco,
                Telefone = EI.Empresa.Telefone,
                Email = EI.Empresa.Email,
                Situacao = EI.Empresa.Situacao,
                Edital = EI.Edital,
                AnoIncubacao = EI.AnoIncubacao,
                UltimaModificacao = EI.Empresa.UltimaModificacao
            };

            Logo = EI.Empresa.Logo;
            return Page();
        }
    }
}
