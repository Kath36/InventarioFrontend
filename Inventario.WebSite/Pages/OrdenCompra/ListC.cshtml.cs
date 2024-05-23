using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Api.Dto;
using Inventario.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventario.WebSite.Pages.OrdenCompra
{
    public class List : PageModel
    {
        private readonly IOrdenCompraService _service;

        public List(IOrdenCompraService service)
        {
            _service = service;
        }

        public List<OrdenCompraDto> OrdenesCompra { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var response = await _service.GetByNameAsync(SearchString);
                OrdenesCompra = new List<OrdenCompraDto>(); // Inicializar la lista
                if (response.Data != null) // Verificar si se encontró una orden de compra
                {
                    OrdenesCompra.Add(response.Data); // Agregar la orden de compra encontrada a la lista
                }
            }
            else
            {
                var response = await _service.GetAllAsync();
                OrdenesCompra = response.Data;
            }

            return Page();
        }
    }
}