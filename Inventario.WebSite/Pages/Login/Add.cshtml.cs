using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using System.Collections.Generic;

namespace Inventario.WebSite.Pages.Usuario
{
    public class Add : PageModel
    {
        [BindProperty]
        public UsuarioDto UsuarioDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        private readonly IUsuarioService _service;

        public Add(IUsuarioService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            UsuarioDto = new UsuarioDto();

            if (id.HasValue)
            {
                var response = await _service.GetById(id.Value);
                UsuarioDto = response.Data;
            }

            if (UsuarioDto == null)
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
            Response<UsuarioDto> response;
            if (UsuarioDto.id > 0)
            {
                response = await _service.SaveAsync(UsuarioDto);
            }
            else
            {
                response = await _service.SaveAsync(UsuarioDto);
            }

            Errors = response.Errors;
            if (Errors.Count > 0)
            {
                return Page();
            }

            UsuarioDto = response.Data;
            return RedirectToPage("./List");
        }
    }
}