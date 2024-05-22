using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using System.Collections.Generic;

namespace Inventario.WebSite.Pages.Proveedor
{
    public class Add : PageModel
    {
        [BindProperty]
        public ProveedorDto ProveedorDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        private readonly IProveedorService _service;

        public Add(IProveedorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            ProveedorDto = new ProveedorDto();

            if (id.HasValue)
            {
                var response = await _service.GetById(id.Value);
                ProveedorDto = response.Data;
            }

            if (ProveedorDto == null)
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
            Response<ProveedorDto> response;
            if (ProveedorDto.id > 0)
            {
                response = await _service.SaveAsync(ProveedorDto);
            }
            else
            {
                response = await _service.SaveAsync(ProveedorDto);
            }

            Errors = response.Errors;
            if (Errors.Count > 0)
            {
                return Page();
            }

            ProveedorDto = response.Data;
            return RedirectToPage("./ListProveedor");
        }
    }
}