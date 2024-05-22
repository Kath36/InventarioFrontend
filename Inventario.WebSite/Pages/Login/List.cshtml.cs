using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;
using System.Threading.Tasks;
using Inventario.Core.Http;
using Microsoft.Extensions.Logging;

namespace Inventario.WebSite.Pages.Usuario
{
    public class LoginModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(IUsuarioService usuarioService, ILogger<LoginModel> logger)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }

        [BindProperty]
        public LoginRequestDto LoginRequest { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Datos del formulario no válidos.";
                return Page();
            }

            try
            {
                var response = await _usuarioService.AuthenticateAsync(LoginRequest.Email, LoginRequest.Contraseña);
                
                if (response == null || response.Data == null)
                {
                    ErrorMessage = "Correo electrónico o contraseña incorrectos.";
                    return Page();
                }
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el proceso de autenticación.");
                ErrorMessage = "Se produjo un error al intentar iniciar sesión. Por favor, inténtelo de nuevo más tarde.";
                return Page();
            }
        }
    }
}
