using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.ViewModels;
using System.Linq;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Incubadas
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IncubadaDetailsVM IncubadaVM { get; set; }
        public int PassoMessage { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? passo)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EI = await _context.DadosIncubadas
                .Include(d => d.Empresa)
                .ThenInclude(e => e.RamoAtuacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.EmpresaID == id);

            if (EI == null)
            {
                return NotFound();
            }

            IncubadaVM = new IncubadaDetailsVM
            {
                ID = EI.EmpresaID,
                Nome = EI.Empresa.Nome,
                CNPJ = EI.Empresa.CNPJ,
                Segmento = EI.Empresa.Segmento,
                RamoAtuacaoCNAE = EI.Empresa.RamoAtuacao.CNAE,
                Descricao = EI.Empresa.Descricao,
                Endereco = EI.Empresa.Endereco,
                Telefone = EI.Empresa.Telefone,
                Email = EI.Empresa.Email,
                Logo = EI.Empresa.Logo,
                Situacao = EI.Empresa.Situacao,
                UltimaModificacao = EI.Empresa.UltimaModificacao,
                Edital = EI.Edital,
                MesEntrada = EI.MesEntrada,
                MesSaida = EI.MesSaida,
            };

            //IncubadaVM.Contagem = await (
            //    from e in _context.Empresas.Where(d => d.ID == id)
            //    let r = e.RedesSociais.Count
            //    let s = e.ProdServicos.Count
            //    let t = e.TagsAssociadas.Count
            //    select new Contagem {
            //        RedesSociais = r,
            //        ProdServicos = s,
            //        TagsAssociadas = t,
            //    }).FirstOrDefaultAsync();

            PassoMessage = passo ?? 0;
            return Page();
        }
    }
}
