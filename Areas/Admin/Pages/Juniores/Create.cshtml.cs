﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.AspNetCore.Hosting;
using EmpreendedorismoEIT.Utils;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Juniores
{
    public class CreateModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(EmpreendedorismoEIT.Data.ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public EmpresaJuniorVM EmpresaJuniorVM { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var novaEmpresa = new Empresa
            {
                Nome = EmpresaJuniorVM.Nome,
                Tipo = Tipo.JUNIOR,
                DescricaoCurta = EmpresaJuniorVM.DescricaoCurta,
                DescricaoLonga = EmpresaJuniorVM.DescricaoLonga,
                Endereco = EmpresaJuniorVM.Endereco,
                Telefone = EmpresaJuniorVM.Telefone,
                Email = EmpresaJuniorVM.Email,
                Logo = Upload.SalvarArquivo(_webHostEnvironment, EmpresaJuniorVM.Logo),
                Situacao = EmpresaJuniorVM.Situacao,
                DadosJunior = new DadosJunior
                {
                    Campus = EmpresaJuniorVM.Campus,
                    Instituto = EmpresaJuniorVM.Instituto
                },
                UltimaModificacao = DateTime.Now
            };

            _context.Empresas.Add(novaEmpresa);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
