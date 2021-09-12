using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.ViewModels;
using System.Linq;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public JuniorDetailsVM JuniorVM { get; set; }

        public int PassoMessage { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? passo)
        {
            if (id == null)
            {
                return NotFound();
            }

            var EJ = await _context.DadosJuniores
                .Include(d => d.Empresa)
                .ThenInclude(e => e.RamoAtuacao)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.EmpresaID == id);

            if (EJ == null)
            {
                return NotFound();
            }

            JuniorVM = new JuniorDetailsVM
            {
                ID = EJ.EmpresaID,
                Nome = EJ.Empresa.Nome,
                CNPJ = EJ.Empresa.CNPJ,
                Segmento = EJ.Empresa.Segmento,
                RamoAtuacaoCNAE = EJ.Empresa.RamoAtuacao.CNAE,
                Descricao = EJ.Empresa.Descricao,
                Endereco = EJ.Empresa.Endereco,
                Telefone = EJ.Empresa.Telefone,
                Email = EJ.Empresa.Email,
                Logo = EJ.Empresa.Logo,
                Situacao = EJ.Empresa.Situacao,
                UltimaModificacao = EJ.Empresa.UltimaModificacao,
                Campus = EJ.Campus,
                Instituto = EJ.Instituto,
            };

            //JuniorVM.Contagem = await (
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
