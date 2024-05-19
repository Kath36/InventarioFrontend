using Inventario.Core.Http;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Services
{
    public interface IProductCategoryService
    {
        Task<Response<List<ProductCategoryDto>>> GetAllAsync();

        Task<Response<ProductCategoryDto>> GetById(int id);

        Task<Response<ProductCategoryDto>> SaveAsync(ProductCategoryDto productCategoryDto);

        Task<Response<ProductCategoryDto>> UpdateAsync(ProductCategoryDto productCategoryDto);

        Task<Response<bool>> DeleteAsync(int id);
    }
}
