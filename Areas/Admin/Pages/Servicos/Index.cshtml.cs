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

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Servicos
{
    public class IndexModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public IndexModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ServicosVM> ListaProdServ { get; set; }
        public ServicosVM ProdServVM { get; set; }
        public string NomeEmpresa { get; set; }
        public string ReturnURL { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? deleteError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                           .Include(e => e.ProdutosServicos)
                           .AsNoTracking()
                           .FirstOrDefaultAsync(m => m.ID == id);

            if (empresa == null)
            {
                return NotFound();
            }

            if (deleteError.GetValueOrDefault())
            {
                ErrorMessage = Resources.ValidationResources.ErrDelete;
            }

            //ListaProdServ = empresa.ProdutosServicos.OrderBy(s => s.Nome).ToList();
            NomeEmpresa = empresa.Nome;
            ListaProdServ = empresa.ProdutosServicos.Select(ps => new ServicosVM
            {
                ID = ps.ID,
                EmpresaID = ps.EmpresaID,
                Nome = ps.Nome,
                Descricao = ps.Descricao
            }).ToList();

            //Botão voltar e títulos
            ReturnURL = "/Index";
            if (empresa.Tipo == Tipo.JUNIOR)
            {
                ViewData["Section"] = "Juniores";
                ViewData["SectionTitle"] = "Empresa júnior » Produtos e serviços";
                ReturnURL = "/Juniores/Index";
            }
            if (empresa.Tipo == Tipo.INCUBADA)
            {
                ViewData["Section"] = "Incubadas";
                ViewData["SectionTitle"] = "Empresa incubada » Produtos e serviços";
                ReturnURL = "/Incubadas/Index";
            }

            return Page();
        }
    }
}
