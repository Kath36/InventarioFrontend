using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Pages.Brands;

public class Edit : PageModel
{
    [BindProperty] public BrandDto BrandDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IBrandsService _service;

    public Edit(IBrandsService service)
    {
        _service = service;
    }

    public  async Task<IActionResult> OnGet(int? id)
    {
        BrandDto = new BrandDto();

        if (id.HasValue)
        {
            var response = await _service.GetById(id.Value);
            BrandDto = response.Data;
        }

        if (BrandDto == null)
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
        Response<BrandDto> response;
        if (BrandDto.id > 0)
        {
            response = await _service.UpdateAsync(BrandDto);
        }
        else
        {
            response = await _service.SaveAsync(BrandDto);
        }

        BrandDto = response.Data;
        return RedirectToPage("./ListBrands");
    }
}