using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Entities;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Pages.Brands;

public class Delete : PageModel
{
    
    [BindProperty] public BrandDto BrandDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IBrandsService _service;

    public Delete(IBrandsService service)
    {
        _service = service;
    }
    public async Task<IActionResult>OnGet(int id)
    {
        BrandDto = new BrandDto();
        var response = await _service.GetById(id);
        BrandDto = response.Data;

        if (BrandDto == null)
        {
            return RedirectToPage("/Error");
            
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(BrandDto.id);
        return RedirectToPage("./ListBrands");
    }
}