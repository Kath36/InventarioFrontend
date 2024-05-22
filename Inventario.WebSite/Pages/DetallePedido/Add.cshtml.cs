using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebSite.Pages.DetallePedido
{
    public class Add : PageModel
    {
        [BindProperty]
        public DetallePedidoDto DetallePedidoDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        private readonly IDetallePedidoService _service;

        public Add(IDetallePedidoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            DetallePedidoDto = new DetallePedidoDto();

            if (id.HasValue)
            {
                var response = await _service.GetById(id.Value);
                DetallePedidoDto = response.Data;
            }

            if (DetallePedidoDto == null)
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
            Response<DetallePedidoDto> response;
            if (DetallePedidoDto.id > 0)
            {
                response = await _service.SaveAsync(DetallePedidoDto);
            }
            else
            {
                response = await _service.SaveAsync(DetallePedidoDto);
            }

            Errors = response.Errors;
            if (Errors.Count > 0)
            {
                return Page();
            }

            DetallePedidoDto = response.Data;
            return RedirectToPage("./LiistDetallePedido");
        }
    }
}