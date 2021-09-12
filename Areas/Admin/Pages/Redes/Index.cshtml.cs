using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.Extensions.Logging;
using System;

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
        public int PassoMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? passo)
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
                WhatsappNUM = Empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.WHATSAPP)?.URL,
                TwitterURL = Empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.TWITTER)?.URL,
                LinkedinURL = Empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.LINKEDIN)?.URL,
                Contagem = Empresa.RedesSociais.Count,
            };

            PassoMessage = passo ?? 0;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int? passo)
        {
            if (id == null)
            {
                return NotFound();
            }

            PassoMessage = passo ?? 0;
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
            if (SocialVM.WhatsappNUM != null)
            {
                Empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.WHATSAPP, URL = SocialVM.WhatsappNUM });
            }
            if (SocialVM.TwitterURL != null)
            {
                Empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.TWITTER, URL = SocialVM.TwitterURL });
            }
            if (SocialVM.LinkedinURL != null)
            {
                Empresa.RedesSociais.Add(new RedeSocial {
                    Plataforma = Plataforma.LINKEDIN, URL = SocialVM.LinkedinURL });
            }
            Empresa.UltimaModificacao = DateTime.Now;

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


            if (PassoMessage == 4)
            {
                return RedirectToPage("/EmpTags/Index", new { id = id, passo = 5 });
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
