using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.WebSite.Services;
using System.Collections.Generic;
using Inventario.Core.Http;

namespace Inventario.WebSite.Pages.Proveedor
{
    public class Edit : PageModel
    {
        private readonly IProveedorService _service;

        [BindProperty]
        public ProveedorDto ProveedorDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public Edit(IProveedorService service)
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
                response = await _service.UpdateAsync(ProveedorDto);
            }
            else
            {
                response = await _service.SaveAsync(ProveedorDto);
            }

            ProveedorDto = response.Data;
            return RedirectToPage("./ListProveedor");
        }
    }
}