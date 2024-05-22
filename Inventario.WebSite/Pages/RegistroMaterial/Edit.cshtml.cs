using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Pages.RegistroMaterial
{
    public class Edit : PageModel
    {
        [BindProperty]
        public RegistroMaterialDto RegistroMaterialDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        private readonly IRegistroMaterialService _service;

        public Edit(IRegistroMaterialService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            RegistroMaterialDto = new RegistroMaterialDto();

            if (id.HasValue)
            {
                var response = await _service.GetById(id.Value);
                RegistroMaterialDto = response.Data;
            }

            if (RegistroMaterialDto == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Response<RegistroMaterialDto> response;
            if (RegistroMaterialDto.id > 0)
            {
                response = await _service.UpdateAsync(RegistroMaterialDto);
            }
            else
            {
                response = await _service.SaveAsync(RegistroMaterialDto);
            }

            Errors = response.Errors;
            if (Errors.Count > 0)
            {
                return Page();
            }

            RegistroMaterialDto = response.Data;
            return RedirectToPage("./ListRegistroMaterial");
        }
    }
}