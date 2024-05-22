using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Pages.RegistroMaterial
{
    public class Delete : PageModel
    {
        [BindProperty]
        public RegistroMaterialDto RegistroMaterialDto { get; set; }

        private readonly IRegistroMaterialService _service;

        public Delete(IRegistroMaterialService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var response = await _service.GetById(id);
            RegistroMaterialDto = response.Data;

            if (RegistroMaterialDto == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _service.DeleteAsync(RegistroMaterialDto.id);
            return RedirectToPage("./ListRegistroMaterial");
        }
    }
}