using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.WebSite.Services;

namespace Inventario.WebSite.Pages.Pedido
{
    public class Delete : PageModel
    {
        private readonly IPedidoService _service;

        [BindProperty]
        public PedidoDto PedidoDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public Delete(IPedidoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            PedidoDto = new PedidoDto();
            var response = await _service.GetById(id);
            PedidoDto = response.Data;

            if (PedidoDto == null)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _service.DeleteAsync(PedidoDto.id);
            return RedirectToPage("./ListPedido");
        }
    }
}