using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EmpreendedorismoEIT.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "nonadmin")]
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;

        public DeletePersonalDataModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<DeletePersonalDataModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if (!await _userManager.CheckPasswordAsync(user, Input.Password))
            {
                StatusMessage = ValidationResources.ErrDeleteUsuario;
                return RedirectToPage("./PersonalData");
            }

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                StatusMessage = ValidationResources.ErrDeleteUsuario;
                return RedirectToPage("./PersonalData");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogInformation("[DEBUG] User delete: " + error);
                }
                StatusMessage = ValidationResources.ErrDeleteUsuario;
                return RedirectToPage("./PersonalData");
            }

            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
    }
}
