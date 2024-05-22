using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Api.Dto;
using Inventario.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventario.WebSite.Pages.RegistroMaterial
{
    public class ListModel : PageModel
    {
        private readonly IRegistroMaterialService _service;

        public ListModel(IRegistroMaterialService service)
        {
            _service = service;
        }

        public List<RegistroMaterialDto> RegistrosMaterial { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var response = await _service.GetByNameAsync(SearchString);
                if (response.Data != null)
                {
                    RegistrosMaterial = new List<RegistroMaterialDto> { response.Data };
                }
                else
                {
                    RegistrosMaterial = new List<RegistroMaterialDto>();
                }
            }
            else
            {
                var response = await _service.GetAllAsync();
                RegistrosMaterial = response.Data;
            }

            return Page();
        }
    }
}