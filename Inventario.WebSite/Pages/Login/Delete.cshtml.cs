using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.WebSite.Services;

namespace Inventario.WebSite.Pages.Usuario
{
    public class Delete : PageModel
    {
        private readonly IUsuarioService _service;

        [BindProperty]
        public UsuarioDto UsuarioDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public Delete(IUsuarioService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            UsuarioDto = new UsuarioDto();
            var response = await _service.GetById(id);
            UsuarioDto = response.Data;

            if (UsuarioDto == null)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _service.DeleteAsync(UsuarioDto.id);
            return RedirectToPage("./List");
        }
    }
}