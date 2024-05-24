using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebSite.Pages.OrdenCompra
{
    public class Add : PageModel
    {
        [BindProperty]
        public OrdenCompraDto OrdenCompraDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        private readonly IOrdenCompraService _service;

        public Add(IOrdenCompraService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            OrdenCompraDto = new OrdenCompraDto();

            if (id.HasValue)
            {
                var response = await _service.GetByIdAsync(id.Value);
                OrdenCompraDto = response.Data;
            }

            if (OrdenCompraDto == null)
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
            Response<OrdenCompraDto> response;
            if (OrdenCompraDto.id > 0)
            {
                response = await _service.SaveAsync(OrdenCompraDto);
            }
            else
            {
                response = await _service.SaveAsync(OrdenCompraDto);
            }

            Errors = response.Errors;
            if (Errors.Count > 0)
            {
                return Page();
            }

            OrdenCompraDto = response.Data;
            return RedirectToPage("./ListC");
        }
    }
}