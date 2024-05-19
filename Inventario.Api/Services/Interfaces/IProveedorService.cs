using Inventario.Api.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Services.Interfaces
{
    public interface IProveedorService
    {
        Task<bool> ProveedorExists(int id);
        
        Task<ProveedorDto> SaveAsync(ProveedorDto proveedorDto);
        
        Task<ProveedorDto> UpdateAsync(ProveedorDto proveedorDto);
        
        Task<List<ProveedorDto>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<ProveedorDto> GetByIdAsync(int id);
    }
}


