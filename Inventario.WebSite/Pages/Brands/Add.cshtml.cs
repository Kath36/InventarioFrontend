using Inventario.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventario.Core.Http;
using Inventario.WebSite.Services;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Pages.Brand;

public class Add : PageModel
{
    [BindProperty] public BrandDto BrandDto { get; set; }

    public List<string> Errors { get; set; } = new List<string>();
    private readonly IBrandsService _service;

    public Add(IBrandsService service)
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
            response = await _service.SaveAsync(BrandDto);
        }
        else
        {
            response = await _service.SaveAsync(BrandDto);
        }

        Errors = response.Errors;
        if (Errors.Count > 0)
        {
            return Page();
        }

        BrandDto = response.Data;
        return RedirectToPage("./ListBrands");
    }
  
}