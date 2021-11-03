using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.Extensions.Configuration;
using EmpreendedorismoEIT.Utils;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public PaginatedList<JuniorListaVM> ListaEJ { get;set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public Situacao CurrentSituacao { get; set; }

        public async Task OnGetAsync(string search, string filter, int? pageIndex, int? status, string sort)
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
            CurrentSort = sort;

            var empresasIQ = _context.DadosJuniores.Select(e => new JuniorListaVM
            {
                ID = e.EmpresaID,
                Logo = e.Empresa.Logo,
                Nome = e.Empresa.Nome,
                Campus = e.Campus,
                Situacao = e.Empresa.Situacao,
                UltimaModificacao = e.Empresa.UltimaModificacao
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

            //Fazer ordenação
            switch (sort)
            {
                case "nome":
                    empresasIQ = empresasIQ.OrderBy(e => e.Nome);
                    break;
                default:
                    empresasIQ = empresasIQ.OrderByDescending(e => e.UltimaModificacao);
                    break;
            }

            //Fazer consulta paginada
            var pageSize = _configuration.GetValue("PageSize", 4);
            ListaEJ = await PaginatedList<JuniorListaVM>.CreateAsync(
                empresasIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
