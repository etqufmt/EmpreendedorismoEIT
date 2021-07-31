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
        public Situacao CurrentSituacao { get; set; }

        public async Task OnGetAsync(string search, string filter, int? pageIndex, int? status)
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
                Campus = e.Campus,
                Situacao = e.Empresa.Situacao
            });

            //Filtar por situação
            if (status != null)
            {
                if (Enum.IsDefined(typeof(Situacao), status))
                {
                    CurrentSituacao = (Situacao)status;
                    empresasIQ = empresasIQ.Where(e => e.Situacao == CurrentSituacao);
                }
            }

            //Filtrar por pesquisa
            if (!String.IsNullOrEmpty(CurrentFilter))
            {
                empresasIQ = empresasIQ.Where(e => e.Nome.Contains(CurrentFilter));
            }

            empresasIQ = empresasIQ.OrderBy(e => e.Nome);

            //Fazer consulta paginada
            var pageSize = _configuration.GetValue("PageSize", 4);
            ListaEJ = await PaginatedList<JunioresListaVM>.CreateAsync(
                empresasIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
