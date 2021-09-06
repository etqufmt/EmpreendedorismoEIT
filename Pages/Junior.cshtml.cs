using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EmpreendedorismoEIT.Pages
{
    public class JuniorModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public JuniorModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DadosJunior> ListaAraguia { get; set; }

        public List<DadosJunior> ListaCuiaba { get; set; }
        
        public List<DadosJunior> ListaSinop { get; set; }

        public async Task OnGetAsync()
        {
            var listaEmp = await _context.DadosJuniores
                .Include(d => d.Empresa)
                .Where(d => d.Empresa.Situacao == Situacao.ATIVA)
                .OrderBy(d => d.Empresa.Nome).AsNoTracking().ToListAsync();

            ListaAraguia = listaEmp.Where(e => e.Campus == Campus.ARAGUAIA).ToList();
            ListaCuiaba = listaEmp.Where(e => e.Campus == Campus.CUIABA).ToList();
            ListaSinop = listaEmp.Where(e => e.Campus == Campus.SINOP).ToList();
        }
    }
}
