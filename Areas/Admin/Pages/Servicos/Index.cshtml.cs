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
        public Empresa Empresa { get; set; }
        public string ReturnURL { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, bool? deleteError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresas
                           .Include(e => e.ProdutosServicos)
                           .AsNoTracking()
                           .FirstOrDefaultAsync(m => m.ID == id);
            if (Empresa == null)
            {
                return NotFound();
            }

            if (deleteError.GetValueOrDefault())
            {
                ErrorMessage = Resources.ValidationResources.ErrDelete;
            }

            ListaProdServ = Empresa.ProdutosServicos.Select(ps => new ServicosVM
            {
                ID = ps.ID,
                EmpresaID = ps.EmpresaID,
                Nome = ps.Nome,
                Descricao = ps.Descricao
            }).OrderBy(ps => ps.Nome).ToList();

            //Botão voltar e títulos
            if (Empresa.Tipo == Tipo.JUNIOR)
            {
                ViewData["Section"] = "Juniores";
                ViewData["SectionTitle"] = "Empresa júnior » Produtos e serviços";
                ReturnURL = "/Juniores/Index";
            }
            if (Empresa.Tipo == Tipo.INCUBADA)
            {
                ViewData["Section"] = "Incubadas";
                ViewData["SectionTitle"] = "Empresa incubada » Produtos e serviços";
                ReturnURL = "/Incubadas/Index";
            }

            return Page();
        }
    }
}
