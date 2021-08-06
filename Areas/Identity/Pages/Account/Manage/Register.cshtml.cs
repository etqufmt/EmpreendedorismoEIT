using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EmpreendedorismoEIT.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace EmpreendedorismoEIT.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "admin")]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList UsuariosSL { get; set; }

        public class InputModel
        {
            [Display(Name = "Usuário")]
            [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
            [StringLength(30, ErrorMessage = "No mínimo {2} e no máximo {1} caracteres", MinimumLength = 6)]
            [RegularExpression(@"^[a-zA-Z0-9-_\.]*$", ErrorMessage = "Usuário inválido")]
            public string UserName { get; set; }

            [Display(Name = "Senha")]
            [Required(ErrorMessageResourceName = "Requerido", ErrorMessageResourceType = typeof(ValidationResources))]
            [StringLength(100, ErrorMessage = "No mínimo {2} caracteres", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Confirmar senha")]
            [Compare("Password", ErrorMessage = "As senhas não conferem")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            await LoadAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadAsync();
                return Page();
            }

            var user = new IdentityUser { UserName = Input.UserName };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateUserName")
                    {
                        StatusMessage = ValidationResources.ErrUsuarioDuplicado;
                    }
                    else
                    {
                        StatusMessage = ValidationResources.ErrCreateUsuario;
                    }
                    _logger.LogInformation("[DEBUG] User create: " + error.Description);
                }
                return RedirectToPage();
            }

            await _userManager.AddToRoleAsync(user, "nonadmin");
            StatusMessage = ValidationResources.SucCreateUsuario;
            return RedirectToPage();
        }

        private async Task LoadAsync()
        {
            var listaUsuarios = await _userManager.GetUsersInRoleAsync("nonadmin");
            UsuariosSL = new SelectList(listaUsuarios.OrderBy(u => u.NormalizedUserName), "UserName", "UserName");
        }
    }
}
