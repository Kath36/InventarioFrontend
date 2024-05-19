using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using System.Collections.Generic;

namespace Inventario.WebSite.Pages.Material
{
    public class Add : PageModel
    {
        [BindProperty]
        public MaterialDto MaterialDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        private readonly IMaterialService _service;

        public Add(IMaterialService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            MaterialDto = new MaterialDto();

            if (id.HasValue)
            {
                var response = await _service.GetById(id.Value);
                MaterialDto = response.Data;
            }

            if (MaterialDto == null)
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
            Response<MaterialDto> response;
            if (MaterialDto.id > 0)
            {
                response = await _service.SaveAsync(MaterialDto);
            }
            else
            {
                response = await _service.SaveAsync(MaterialDto);
            }

            Errors = response.Errors;
            if (Errors.Count > 0)
            {
                return Page();
            }

            MaterialDto = response.Data;
            return RedirectToPage("./ListMaterial");
        }
    }
}