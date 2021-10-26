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

            var listaEmp = await MatchManager.ObterRecomendacao(_context, _logger, listaTagsID);
            //var listaEmpID = new List<int> { 1, 2, 3 };
            if (listaEmp == null)
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
                    PorcentagemMatch = listaEmp[e.ID],
                    RedesSociais = e.RedesSociais
                })
                .AsNoTracking()
                .ToListAsync();

            if (ListaResEmp.Count == 0)
            {
                return NotFound();
            }

            ListaResEmp = ListaResEmp.OrderByDescending(e => e.PorcentagemMatch).ToList();
            return Page();
        }
    }
}
