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
    public class IndexModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public IndexModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<JunioresListaVM> ListaEJ { get;set; }

        public async Task OnGetAsync()
        {
            //Faz um inner join em vez de buscar tudo
            ListaEJ = await _context.DadosJuniores.Select(e => new JunioresListaVM
            {
                ID = e.EmpresaID,
                Logo = e.Empresa.Logo,
                Nome = e.Empresa.Nome,
                Campus = e.Campus
            }).ToListAsync();
        }
    }
}
