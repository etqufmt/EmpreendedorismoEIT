using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using System;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public TagVM TagVM { get; set; }
        public IList<TagVM> ListaTags { get;set; }

        public async Task OnGetAsync()
        {
            ListaTags = await _context.Tags.Select(t => new TagVM {
                ID = t.ID,
                Nome = t.Nome,
                CorInt = t.Cor,
            }).OrderBy(t => t.Nome).AsNoTracking().ToListAsync();
        }
    }
}
