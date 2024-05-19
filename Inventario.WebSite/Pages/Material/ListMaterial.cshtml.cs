using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Api.Dto;
using Inventario.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventario.WebSite.Pages.Material
{
    public class ListMaterial : PageModel
    {
        private readonly IMaterialService _service;

        public ListMaterial(IMaterialService service)
        {
            _service = service;
        }

        public List<MaterialDto> Materials { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var response = await _service.GetByNameAsync(SearchString);
                Materials = new List<MaterialDto>(); // Inicializar la lista
                if (response.Data != null) // Verificar si se encontró un material
                {
                    Materials.Add(response.Data); // Agregar el material encontrado a la lista
                }
            }
            else
            {
                var response = await _service.GetAllAsync();
                Materials = response.Data;
            }

            return Page();
        }
    }
}