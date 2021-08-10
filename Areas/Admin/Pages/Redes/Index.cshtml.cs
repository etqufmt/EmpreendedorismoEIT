using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Redes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            ApplicationDbContext context,
            ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public RedesVM SocialVM { get;set; }
        public Empresa Empresa { get; set; }
        public string ReturnURL { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresas
                            .Include(e => e.RedesSociais)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (Empresa == null)
            {
                return NotFound();
            }

            SocialVM = new RedesVM
            {
                EmpresaID = Empresa.ID,
                WebsiteURL = Empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.WEBSITE)?.URL,
                FacebookURL = Empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.FACEBOOK)?.URL,
                InstagramURL = Empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.INSTAGRAM)?.URL,
                WhatsappURL = Empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.WHATSAPP)?.URL[2..],
                TwitterURL = Empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.TWITTER)?.URL
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresas
                .Include(e => e.RedesSociais)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Empresa == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Empresa.RedesSociais.Clear();
            if (SocialVM.WebsiteURL != null)
            {
                Empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.WEBSITE, URL = SocialVM.WebsiteURL });
            }
            if (SocialVM.FacebookURL != null)
            {
                Empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.FACEBOOK, URL = SocialVM.FacebookURL });
            }
            if (SocialVM.InstagramURL != null)
            {
                Empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.INSTAGRAM, URL = SocialVM.InstagramURL });
            }
            if (SocialVM.WhatsappURL != null)
            {
                //Código internacional para telefones brasileiros = 55
                Empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.WHATSAPP, URL = SocialVM.WhatsappURL.Insert(0, "55") });
            }
            if (SocialVM.TwitterURL != null)
            {
                Empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.TWITTER, URL = SocialVM.TwitterURL });
            }

            try
            {
                _context.Attach(Empresa).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("[DEBUG] RedesSociais:update // " + ex);
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            if (Empresa.Tipo == Tipo.JUNIOR)
            {
                ReturnURL = "/Juniores/Details";
            }
            if (Empresa.Tipo == Tipo.INCUBADA)
            {
                ReturnURL = "/Incubadas/Details";
            }
            return RedirectToPage(ReturnURL, new { id });
        }
    }
}
