using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Entities;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Pages.ProductCategory;

public class Delete : PageModel
{
    
    [BindProperty] public ProductCategoryDto ProductCategoryDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IProductCategoryService _service;

    public Delete(IProductCategoryService service)
    {
        _service = service;
    }
    public async Task<IActionResult>OnGet(int id)
    {
        ProductCategoryDto = new ProductCategoryDto();
        var response = await _service.GetById(id);
        ProductCategoryDto = response.Data;

        if (ProductCategoryDto == null)
        {
            return RedirectToPage("/Error");
            
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(ProductCategoryDto.id);
        return RedirectToPage("./List");
    }
}