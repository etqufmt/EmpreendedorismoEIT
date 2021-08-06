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

namespace EmpreendedorismoEIT.Areas.Admin.Pages.Tags
{
    public class DeleteUserModel : PageModel
    {
        private readonly EmpreendedorismoEIT.Data.ApplicationDbContext _context;

        public DeleteUserModel(EmpreendedorismoEIT.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TagsVM TagVM { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = TagVM.ID;
            if (id == 0)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            try
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return RedirectToPage("Index", new { deleteError = true });
            }

            return RedirectToPage("Index");
        }
    }
}
