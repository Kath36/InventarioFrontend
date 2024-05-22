using Inventario.Api.Dto;
using Inventario.Core.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebSite.Services
{
    public interface IRegistroMaterialService
    {

        Task<Response<List<RegistroMaterialDto>>> GetAllAsync();

        Task<Response<RegistroMaterialDto>> GetById(int id);

        Task<Response<RegistroMaterialDto>> SaveAsync(RegistroMaterialDto registroMaterialDto);

        Task<Response<RegistroMaterialDto>> UpdateAsync(RegistroMaterialDto registroMaterialDto);

        Task<Response<bool>> DeleteAsync(int id);

        Task<bool> RegistroMaterialExists(int id);

        Task<Response<RegistroMaterialDto>> GetByNameAsync(string name);
    }
}