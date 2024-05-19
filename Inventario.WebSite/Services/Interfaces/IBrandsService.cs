using Inventario.Api.Dto;
using Inventario.Core.Http;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Services;

public interface IBrandsService
{
    Task<Response<List<BrandDto>>> GetAllAsync();

    Task<Response<BrandDto>> GetById(int id);

    Task<Response<BrandDto>> SaveAsync(BrandDto brandDto);

    Task<Response<BrandDto>> UpdateAsync(BrandDto brandDto);

    Task<Response<bool>> DeleteAsync(int id);
}