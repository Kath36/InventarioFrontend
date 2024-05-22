using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Api.Dto;
using Inventario.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventario.WebSite.Pages.DetallePedido
{
    public class ListDetallePedido : PageModel
    {
        private readonly IDetallePedidoService _service;

        public ListDetallePedido(IDetallePedidoService service)
        {
            _service = service;
        }

        public List<DetallePedidoDto> DetallePedidos { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var response = await _service.GetByMaterialIdAsync(int.Parse(SearchString));
                DetallePedidos = new List<DetallePedidoDto>(); // Inicializar la lista
                if (response.Data != null) // Verificar si se encontró un detalle de pedido
                {
                    DetallePedidos.Add(response.Data); // Agregar el detalle de pedido encontrado a la lista
                }
            }
            else
            {
                var response = await _service.GetAllAsync();
                DetallePedidos = response.Data;
            }

            return Page();
        }
    }
}