﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.EmpTags
{
    public class IndexModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public IndexModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<EmpTagsVM> ListaET { get; set; }
        public Empresa Empresa { get; set; }
        public string ReturnURL { get; set; }
        public string ErrorMessage { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id, bool? updateError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresas
                .Include(e => e.TagsAssociadas)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Empresa == null)
            {
                return NotFound();
            }

            if (updateError.GetValueOrDefault())
            {
                ErrorMessage = Resources.ValidationResources.ErrUpdate;
            }

            //Criar lista com todas as tags utilizando as associações que já existem
            var allTags = await _context.Tags.AsNoTracking().ToListAsync();
            ListaET = new List<EmpTagsVM>();
            foreach (var tag in allTags)
            {
                int grau = 0;
                var et = Empresa.TagsAssociadas.FirstOrDefault(et => et.TagID == tag.ID);
                if (et != null)
                {
                    grau = Decimal.ToInt32(et.Grau * 100);
                }
                ListaET.Add(new EmpTagsVM
                {
                    TagID = tag.ID,
                    Grau = grau,
                    Tag = tag,
                });
            }

            //Botão voltar e títulos
            ReturnURL = "/Index";
            if (Empresa.Tipo == Tipo.JUNIOR)
            {
                ViewData["Section"] = "Juniores";
                ReturnURL = "/Juniores/Index";
            }
            if (Empresa.Tipo == Tipo.INCUBADA)
            {
                ViewData["Section"] = "Incubadas";
                ReturnURL = "/Incubadas/Index";
            }

            return Page();
        }
    }
}
