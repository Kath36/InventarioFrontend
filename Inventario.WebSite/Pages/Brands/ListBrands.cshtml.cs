using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.WebSite.Pages.ProductCategory;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Pages.Brands;

public class ListBrands : PageModel
{
   
    private readonly IBrandsService _service;
    public List<BrandDto> brands { get; set; }


    public ListBrands(IBrandsService service)
    {
        brands = new List<BrandDto>();
        _service = service;
    }

    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        if (response != null && response.Data != null)
        {
            brands = response.Data;
        }
        else
        {
            brands = new List<BrandDto>();
        }
        return Page();
    }
}