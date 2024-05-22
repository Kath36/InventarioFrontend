using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using System.Collections.Generic;

namespace Inventario.WebSite.Pages.Pedido
{
    public class Add : PageModel
    {
        [BindProperty]
        public PedidoDto PedidoDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        private readonly IPedidoService _service;

        public Add(IPedidoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            PedidoDto = new PedidoDto();

            if (id.HasValue)
            {
                var response = await _service.GetById(id.Value);
                PedidoDto = response.Data;
            }

            if (PedidoDto == null)
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
            Response<PedidoDto> response;
            if (PedidoDto.id > 0)
            {
                response = await _service.SaveAsync(PedidoDto);
            }
            else
            {
                response = await _service.SaveAsync(PedidoDto);
            }

            Errors = response.Errors;
            if (Errors.Count > 0)
            {
                return Page();
            }

            PedidoDto = response.Data;
            return RedirectToPage("./ListPedido");
        }
    }
}