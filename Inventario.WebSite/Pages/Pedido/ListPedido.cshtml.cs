using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Api.Dto;
using Inventario.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventario.WebSite.Pages.Pedido
{
    public class ListPedido : PageModel
    {
        private readonly IPedidoService _service;

        public ListPedido(IPedidoService service)
        {
            _service = service;
        }

        public List<PedidoDto> Pedidos { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                var response = await _service.GetByNameAsync(SearchString);
                Pedidos = new List<PedidoDto>(); // Inicializar la lista
                if (response.Data != null) // Verificar si se encontró un material
                {
                    Pedidos.Add(response.Data); // Agregar el material encontrado a la lista
                }
            }
            else
            {
                var response = await _service.GetAllAsync();
                Pedidos = response.Data;
            }

            return Page();
        }
    }
}