using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Pages.ProductCategory;

public class Edit : PageModel
{
    [BindProperty] public ProductCategoryDto ProductCategoryDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IProductCategoryService _service;

    public Edit(IProductCategoryService service)
    {
        _service = service;
    }

    public  async Task<IActionResult> OnGet(int? id)
    {
        ProductCategoryDto = new ProductCategoryDto();

        if (id.HasValue)
        {
            //obtener informacion del servicio API
            var response = await _service.GetById(id.Value);
            ProductCategoryDto = response.Data;
        }

        if (ProductCategoryDto == null)
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
        Response<ProductCategoryDto> response;
        if (ProductCategoryDto.id > 0)
        {
            response = await _service.UpdateAsync(ProductCategoryDto);
        }
        else
        {
            response = await _service.SaveAsync(ProductCategoryDto);
        }

        Errors = response.Errors;
        if (Errors.Count > 0)
        {
            return Page();
        }

        ProductCategoryDto = response.Data;
        return RedirectToPage("./list");
    }
  
}