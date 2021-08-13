using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmpreendedorismoEIT.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Dashboard Dashboard { get; set; }

        public async Task OnGetAsync()
        {
            Dashboard = await _context.Dashboard
                .FromSqlRaw(@"SELECT (SELECT COUNT(*) FROM [dbo].[DadosJuniores]) AS Juniores,
                (SELECT COUNT(*) FROM [dbo].[DadosIncubadas]) AS Incubadas,
                (SELECT COUNT(*) FROM [dbo].[Tags]) AS Tags,
                (SELECT COUNT(*) FROM [dbo].[ProdutosServicos]) AS Servicos")
                .FirstOrDefaultAsync();
        }
    }
}
