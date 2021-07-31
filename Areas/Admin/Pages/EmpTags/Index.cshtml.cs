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


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresas.FindAsync(id);

            if (Empresa == null)
            {
                return NotFound();
            }

            //Lista todas as tags cadastradas com ou sem associação
            ListaET = new List<EmpTagsVM>();
            var allTags = await _context.Tags
                .Include(t => t.EmpresasAssociadas.Where(e => e.EmpresaID == Empresa.ID))
                .AsNoTracking()
                .ToListAsync();
            foreach (var tag in allTags)
            {
                int grau = 0;
                var empTag = tag.EmpresasAssociadas.FirstOrDefault();
                if (empTag != null)
                {
                    grau = Decimal.ToInt32(empTag.Grau * 100);
                }
                ListaET.Add(new EmpTagsVM
                {
                    TagID = tag.ID,
                    Nome = tag.Nome,
                    Cor = tag.Cor,
                    Grau = grau,
                });
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Empresa = await _context.Empresas
                .Include(e => e.TagsAssociadas)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Empresa == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            Empresa.TagsAssociadas.Clear();
            Empresa.TagsAssociadas = new List<EmpresaTag>();
            foreach (var et in ListaET)
            {
                //Cadastrar apenas associações com algum grau definido
                if (et.Grau != 0)
                {
                    Empresa.TagsAssociadas.Add(new EmpresaTag
                    {
                        EmpresaID = Empresa.ID,
                        TagID = et.TagID,
                        Grau = (et.Grau / 100M)
                    });
                }
            }

            _context.Attach(Empresa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, Resources.ValidationResources.ErrUpdate);
                return Page();
            }

            if (Empresa.Tipo == Tipo.JUNIOR)
            {
                ReturnURL = "/Juniores/Index";
            }
            if (Empresa.Tipo == Tipo.INCUBADA)
            {
                ReturnURL = "/Incubadas/Index";
            }
            return RedirectToPage(ReturnURL);
        }
    }
}