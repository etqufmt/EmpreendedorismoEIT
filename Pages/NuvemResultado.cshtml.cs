using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.Utils;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Pages
{
    public class NuvemResultadoModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<NuvemResultadoModel> _logger;

        public NuvemResultadoModel(ApplicationDbContext context, ILogger<NuvemResultadoModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string ListaTagsTxt { get; set; }

        public List<EmpCloudVM> ListaResEmp { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (String.IsNullOrEmpty(ListaTagsTxt))
            {
                return NotFound();
            }

            List<int> listaTagsID = MatchManager.ListarInteiros(ListaTagsTxt);
            if (listaTagsID.Count < 3)
            {
                return NotFound();
            }

            Dictionary<int, int> listaEmp = await MatchManager.ObterRecomendacao(_context, _logger, listaTagsID);
            if (listaEmp.Count < 1 || listaEmp.Count > 3)
            {
                return NotFound();
            }

            ListaResEmp = await _context.Empresas
                .Where(e => listaEmp.Keys.ToList().Contains(e.ID))
                .Include(e => e.DadosJunior)
                .Include(e => e.DadosIncubada)
                .Include(e => e.RedesSociais.OrderBy(rs => rs.Plataforma))
                .Select(e => new EmpCloudVM
                {
                    ID = e.ID,
                    Nome = e.Nome,
                    Tipo = e.Tipo,
                    Descricao = e.Descricao,
                    Telefone = TextManager.FormatarTelefone(e.Telefone),
                    Email = e.Email,
                    LogoURL = LogoManager.URLImagem(Url, e.Logo),
                    JunCampus = e.DadosJunior != null ? e.DadosJunior.Campus : 0,
                    IncMesEntrada = e.DadosIncubada != null ? e.DadosIncubada.MesEntrada : DateTime.UnixEpoch,
                    Porcentagem = listaEmp[e.ID],
                    RedesSociais = e.RedesSociais
                })
                .AsNoTracking()
                .ToListAsync();

            if (ListaResEmp.Count == 0)
            {
                return NotFound();
            }

            //Shuffle para alternar a ordem das empresas
            //Quando várias empresas estão empatadas em primeiro lugar
            ListaResEmp = ListaResEmp
                .Shuffle(new Random())
                .OrderByDescending(e => e.Porcentagem)
                .ToList();
            return Page();
        }
    }
}
