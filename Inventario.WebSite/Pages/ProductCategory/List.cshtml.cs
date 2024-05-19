using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Entities;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Pages.ProductCategory;

public class ListModel : PageModel
{

    private readonly IProductCategoryService _service;
    public List<ProductCategoryDto> productCategories { get; set; }


    public ListModel(IProductCategoryService service)
    {
        productCategories = new List<ProductCategoryDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        //La llamada al servicio
        var response = await _service.GetAllAsync();
        productCategories = response.Data;

        return Page();
    }
}