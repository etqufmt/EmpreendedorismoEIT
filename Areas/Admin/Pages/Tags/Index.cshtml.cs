using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EmpreendedorismoEIT.Data;
using EmpreendedorismoEIT.Models;
using EmpreendedorismoEIT.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Tags
{
    public class IndexModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public IndexModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TagsVM TagVM { get; set; }
        public IList<Tag> ListaTags { get;set; }
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(bool? deleteError = false)
        {
            if (deleteError.GetValueOrDefault())
            {
                ErrorMessage = Resources.ValidationResources.ErrDelete;
            }

            ListaTags = await _context.Tags.OrderBy(t => t.Nome).AsNoTracking().ToListAsync();
        }
    }
}
