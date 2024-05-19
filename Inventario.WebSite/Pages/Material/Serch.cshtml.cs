using System.Threading.Tasks;
using Inventario.Api.Dto;
using Inventario.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventario.WebSite.Pages.Material
{
    public class SearchMaterial : PageModel
    {
        private readonly IMaterialService _materialService;

        public SearchMaterial(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        public MaterialDto MaterialDto { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var response = await _materialService.GetByNameAsync(Name);
                MaterialDto = response.Data;

                if (MaterialDto == null)
                {
                    ModelState.AddModelError(string.Empty, $"Material with name '{Name}' not found.");
                }
            }

            return Page();
        }
    }
}