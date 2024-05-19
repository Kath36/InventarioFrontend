using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.WebSite.Services;

namespace Inventario.WebSite.Pages.Material
{
    public class Delete : PageModel
    {
        private readonly IMaterialService _service;

        [BindProperty]
        public MaterialDto MaterialDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public Delete(IMaterialService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            MaterialDto = new MaterialDto();
            var response = await _service.GetById(id);
            MaterialDto = response.Data;

            if (MaterialDto == null)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _service.DeleteAsync(MaterialDto.id);
            return RedirectToPage("./ListMaterial");
        }
    }
}