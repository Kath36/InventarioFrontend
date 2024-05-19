using Inventario.Api.Dto;
using Inventario.Core.Entities;
using Inventario.Api.Services.Interfaces;
using Inventario.Repositories.Interfecies;

namespace Inventario.Api.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;

    public BrandService(IBrandRepository brandRepository)
    {
        //Instancia a brand
        _brandRepository = brandRepository;
    }

    public async Task<bool> BrandExist(int id)
    {
        var brand = await _brandRepository.GetById(id);
        //condicion dentro del retorno para saber si encontrop una
        return (brand != null);
    }

    public async Task<BrandDto> SaveAsycn(BrandDto brandDto)
    {
        var brand = new Brand
        {
            Name = brandDto.Name,
            Description = brandDto.Description,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        brand = await _brandRepository.SaveAsycn(brand);
        brandDto.id = brand.id;

        return brandDto;
    }

    public async Task<BrandDto> UpdateAsync(BrandDto brandDto)
    {
        var brand = await _brandRepository.GetById(brandDto.id);
        if (brand == null)
            throw new Exception("Brand Not Found");
        
        brand.Name = brandDto.Name;
        brand.Description = brandDto.Description;
        brand.UpdatedBy = "";
        brand.UpdatedDate = DateTime.Now;
        
        await _brandRepository.UpdateAsync(brand);
        return brandDto;
    }

    public async Task<List<BrandDto>> GetAllAsync()
    {
        var brands = await _brandRepository.GetAllAsync();
        var brandsDto = brands.Select(c => new BrandDto(c)).ToList();
        return brandsDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _brandRepository.DeleteAsync(id);
    }

    public async Task<BrandDto> GetById(int id)
    {
        var brand = await _brandRepository.GetById(id);
        if (brand == null)
            throw new Exception("Brand not found");

        var brandDto = new BrandDto(brand);
        return brandDto;
    }
}