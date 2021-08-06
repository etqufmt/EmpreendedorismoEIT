using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EmpreendedorismoEIT.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [Display(Name = "Usuário")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Senha atual")]
            [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
            [DataType(DataType.Password)]
            public string OldPassword { get; set; }

            [Display(Name = "Nova senha")]
            [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
            [StringLength(100, ErrorMessage = "No mínimo {2} e no máximo {1} caracteres", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; }

            [Display(Name = "Confirmar nova senha")]
            [Compare("NewPassword", ErrorMessage = "As senhas não conferem")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var result = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _logger.LogInformation("[DEBUG] User update: " + error);
                }
                StatusMessage = ValidationResources.ErrAlterarSenha;
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = ValidationResources.SucAlterarSenha;
            return RedirectToPage();
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            Username = userName;
        }
    }
}
