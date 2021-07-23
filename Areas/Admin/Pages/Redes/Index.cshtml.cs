﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using System.Collections;
using EmpreendedorismoEIT.ViewModels;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Redes
{
    public class IndexModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public IndexModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RedesVM SocialVM { get;set; }
        public string NomeEmpresa { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                            .Include(e => e.RedesSociais)
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (empresa == null)
            {
                return NotFound();
            }

            NomeEmpresa = empresa.Nome;
            SocialVM = new RedesVM
            {
                EmpresaID = empresa.ID,
                WebsiteURL = empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.WEBSITE)?.URL,
                FacebookURL = empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.FACEBOOK)?.URL,
                InstagramURL = empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.INSTAGRAM)?.URL,
                WhatsappURL = empresa.RedesSociais
                                .FirstOrDefault(r => r.Plataforma == Plataforma.WHATSAPP)?.URL,
                TwitterURL = empresa.RedesSociais
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

            var empresa = await _context.Empresas
                            .Include(e => e.RedesSociais)
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (empresa == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            empresa.RedesSociais.Clear();
            if (SocialVM.WebsiteURL != null)
            {
                empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.WEBSITE, URL = SocialVM.WebsiteURL });
            }
            if (SocialVM.FacebookURL != null)
            {
                empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.FACEBOOK, URL = SocialVM.FacebookURL });
            }
            if (SocialVM.InstagramURL != null)
            {
                empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.INSTAGRAM, URL = SocialVM.InstagramURL });
            }
            if (SocialVM.WhatsappURL != null)
            {
                empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.WHATSAPP, URL = SocialVM.WhatsappURL });
            }
            if (SocialVM.TwitterURL != null)
            {
                empresa.RedesSociais.Add(new RedeSocial { 
                    Plataforma = Plataforma.TWITTER, URL = SocialVM.TwitterURL });
            }

            _context.Attach(empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
