using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.WebSite.Services;

namespace Inventario.WebSite.Pages.Proveedor
{
    public class Delete : PageModel
    {
        private readonly IProveedorService _service;

        [BindProperty]
        public ProveedorDto ProveedorDto { get; set; }

        public Delete(IProveedorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            ProveedorDto = new ProveedorDto();
            var response = await _service.GetById(id);
            ProveedorDto = response.Data;

            if (ProveedorDto == null)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _service.DeleteAsync(ProveedorDto.id);
            return RedirectToPage("./ListProveedor");
        }
    }
}