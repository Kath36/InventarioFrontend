using Inventario.Api.Dto;
using Inventario.Api.Dto;

namespace Inventario.Api.Services.Interfaces;

public interface IBrandService
{
    Task<bool> BrandExist(int id);

    Task<List<BrandDto>> GetAllAsync();


    Task<BrandDto> SaveAsycn(BrandDto brand);

    Task<BrandDto> GetById(int id);

    Task<BrandDto> UpdateAsync(BrandDto brand);

    Task<bool> DeleteAsync(int id);
    
}