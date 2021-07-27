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

        public async Task OnGetAsync(string error)
        {
            if (error != null )
            {
                if (error.Equals("create"))
                {
                    ErrorMessage = Resources.ValidationResources.ErrCreate;
                }
                if (error.Equals("edit"))
                {
                    ErrorMessage = Resources.ValidationResources.ErrUpdate;
                }
                if (error.Equals("delete"))
                {
                    ErrorMessage = Resources.ValidationResources.ErrDelete;
                }
            }

            ListaTags = await _context.Tags.OrderBy(t => t.Nome).AsNoTracking().ToListAsync();
        }
    }
}
