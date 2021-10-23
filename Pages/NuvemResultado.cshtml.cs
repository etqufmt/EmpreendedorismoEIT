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

namespace EmpreendedorismoEIT.Pages
{
    public class NuvemResultadoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public NuvemResultadoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string ListaTagsTxt { get; set; }

        //public EmpCloudVM ResEmp { get; set; }
        public List<EmpCloudVM> ListaResEmp { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(ListaTagsTxt))
            {
                return NotFound();
            }

            List<int> listaTagsID = TextManager.ListarInteiros(ListaTagsTxt);
            if (listaTagsID.Count < 3)
            {
                return NotFound();
            }

            //var emp = MatchManager.obterRecomendacao(ListaTagsID);
            //ResEmp = await _context.Empresas
            //    .Where(e => e.Situacao == Situacao.ATIVA)
            //    .Include(e => e.DadosJunior)
            //    .Include(e => e.DadosIncubada)
            //    .Include(e => e.RedesSociais.OrderBy(rs => rs.Plataforma))
            //    .Select(e => new EmpCloudVM {
            //        ID = e.ID,
            //        Nome = e.Nome,
            //        Tipo = e.Tipo,
            //        Descricao = e.Descricao,
            //        Telefone = TextManager.FormatarTelefone(e.Telefone),
            //        Email = e.Email,
            //        LogoURL = LogoManager.URLImagem(Url, e.Logo),
            //        JunCampus = e.DadosJunior != null ? e.DadosJunior.Campus : 0,
            //        IncMesEntrada = e.DadosIncubada != null ? e.DadosIncubada.MesEntrada : DateTime.UnixEpoch,
            //        PorcentagemMatch = 95,
            //        RedesSociais = e.RedesSociais
            //    })
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(e => e.Tipo == Tipo.JUNIOR);
            var listaEmpID = new List<int> { 1, 2, 3 };
            ListaResEmp = await _context.Empresas
                .Where(e => listaEmpID.Contains(e.ID))
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
                    PorcentagemMatch = 95,
                    RedesSociais = e.RedesSociais
                })
                .AsNoTracking()
                .ToListAsync();

            if (ListaResEmp.Count == 0)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
