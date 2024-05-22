using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Api.Dto;
using Inventario.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventario.WebSite.Pages.Proveedor
{
    public class ListProveedor : PageModel
    {
        private readonly IProveedorService _service;

        public ListProveedor(IProveedorService service)
        {
            _service = service;
        }

        public List<ProveedorDto> Proveedores { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var response = await _service.GetByNameAsync(SearchString);
                Proveedores = new List<ProveedorDto>(); // Inicializar la lista
                if (response.Data != null) // Verificar si se encontró un proveedor
                {
                    Proveedores.Add(response.Data); // Agregar el proveedor encontrado a la lista
                }
            }
            else
            {
                var response = await _service.GetAllAsync();
                Proveedores = response.Data;
            }

            return Page();
        }
    }
}