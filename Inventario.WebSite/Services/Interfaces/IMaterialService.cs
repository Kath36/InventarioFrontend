using Inventario.Api.Dto;
using Inventario.Core.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebSite.Services
{
    public interface IMaterialService
    {
        Task<Response<List<MaterialDto>>> GetAllAsync();

        Task<Response<MaterialDto>> GetById(int id);

        Task<Response<MaterialDto>> SaveAsync(MaterialDto materialDto);

        Task<Response<MaterialDto>> UpdateAsync(MaterialDto materialDto);

        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<MaterialDto>> GetByNameAsync(string name);

    }
}