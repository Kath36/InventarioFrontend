using Inventario.Api.Dto;
using Inventario.Core.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebSite.Services
{
    public interface IProveedorService
    {
        Task<Response<List<ProveedorDto>>> GetAllAsync();
        Task<Response<ProveedorDto>> GetById(int id);
        Task<Response<ProveedorDto>> SaveAsync(ProveedorDto proveedorDto);
        Task<Response<ProveedorDto>> UpdateAsync(ProveedorDto proveedorDto);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<ProveedorDto>> GetByNameAsync(string name);
    }
}