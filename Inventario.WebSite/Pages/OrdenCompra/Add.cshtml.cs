using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebSite.Pages.OrdenCompra
{
    public class AddModel : PageModel
    {
        [BindProperty] public OrdenCompraDto OrdenCompraDto { get; set; }

        public List<string> Errors { get; set; } = new List<string>();
        public List<MaterialDto> MaterialOptions { get; set; } = new List<MaterialDto>();
        public List<ProveedorDto> ProveedorOptions { get; set; } = new List<ProveedorDto>();

        private readonly IOrdenCompraService _service;
        private readonly IMaterialService _materialService;
        private readonly IProveedorService _proveedorService;


        public AddModel(IOrdenCompraService service, IMaterialService materialService,
            IProveedorService proveedorService)
        {
            _service = service;
            _materialService = materialService;
            _proveedorService = proveedorService;
        }

        public async Task<IActionResult> OnGet(int? id)
        {
            var materialResponse = await _materialService.GetAllAsync();
            if (materialResponse.Success)
            {
                MaterialOptions = materialResponse.Data;
            }
            else
            {

                MaterialOptions = new List<MaterialDto>();
            }

            var proveedorResponse = await _proveedorService.GetAllAsync();
            if (proveedorResponse.Success)
            {
                ProveedorOptions = proveedorResponse.Data;
            }
            else
            {
                // Manejar el caso en que la llamada no tenga éxito
                // Puedes establecer una lista vacía o manejar el error de otra manera
                ProveedorOptions = new List<ProveedorDto>();
            }

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

            OrdenCompraDto.FechaOrden = DateTime.Now;

            Response<OrdenCompraDto> response;

            if (OrdenCompraDto.id > 0)
            {
                response = await _service.UpdateAsync(OrdenCompraDto);
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