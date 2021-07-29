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
using Microsoft.Extensions.Configuration;
using ContosoUniversity.Utils;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public PaginatedList<JunioresListaVM> ListaEJ { get;set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string search, string filter, int? pageIndex)
        {
            if (search != null)
            {
                pageIndex = 1;
            }
            else
            {
                search = filter;
            }
            CurrentFilter = search;

            var empresasIQ = _context.DadosJuniores.Select(e => new JunioresListaVM
            {
                ID = e.EmpresaID,
                Logo = e.Empresa.Logo,
                Nome = e.Empresa.Nome,
                Campus = e.Campus
            });

            //Filtrar por pesquisa
            if (!String.IsNullOrEmpty(search))
            {
                empresasIQ = empresasIQ.Where(e => e.Nome.Contains(search));
            }

            //Fazer consulta paginada
            var pageSize = _configuration.GetValue("PageSize", 4);
            ListaEJ = await PaginatedList<JunioresListaVM>.CreateAsync(
                empresasIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
