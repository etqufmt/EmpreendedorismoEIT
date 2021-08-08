using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using EmpreendedorismoEIT.Resources;
using Microsoft.AspNetCore.Authorization;

namespace EmpreendedorismoEIT.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "admin")]
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<DeleteUserModel> _logger;


        public DeleteUserModel(
            UserManager<IdentityUser> userManager,
            ILogger<DeleteUserModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel DeleteUser { get; set; }

        public class InputModel
        {
            public string UserName { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var username = DeleteUser.UserName;
            if (String.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            if(await _userManager.IsInRoleAsync(user, "admin"))
            {
                StatusMessage = ValidationResources.ErrDeleteUsuario;
                return RedirectToPage("./Register");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogInformation("[DEBUG] User delete: " + error);
                }
                StatusMessage = ValidationResources.ErrDeleteUsuario;
                return RedirectToPage("./Register");
            }

            StatusMessage = ValidationResources.SucDeleteUsuario;
            return RedirectToPage("./Register");
        }
    }
}
