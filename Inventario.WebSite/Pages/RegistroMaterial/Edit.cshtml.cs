using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebSite.Pages.RegistroMaterial
{
    public class EditModel : PageModel
    {
        private readonly IRegistroMaterialService _service;

        public EditModel(IRegistroMaterialService service)
        {
            _service = service;
        }

        [BindProperty]
        public RegistroMaterialDto RegistroMaterialDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id.HasValue)
            {
                var response = await _service.GetById(id.Value);
                if (response != null && response.Data != null)
                {
                    RegistroMaterialDto = response.Data;
                }
                else
                {
                    return RedirectToPage("/Error");
                }
            }
            else
            {
                RegistroMaterialDto = new RegistroMaterialDto();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Response<RegistroMaterialDto> response;
            if (RegistroMaterialDto.id > 0)
            {
                response = await _service.UpdateAsync(RegistroMaterialDto);
            }
            else
            {
                response = await _service.SaveAsync(RegistroMaterialDto);
            }

            if (response.Errors != null && response.Errors.Count > 0)
            {
                Errors.AddRange(response.Errors);
                return Page();
            }

            return RedirectToPage("./ListRegistroMaterial");
        }
    }
}
