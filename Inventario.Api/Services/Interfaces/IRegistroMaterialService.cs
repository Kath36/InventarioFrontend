using Inventario.Api.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Services.Interfaces
{
    public interface IRegistroMaterialService
    {
        Task<bool> RegistroMaterialExists(int id);
        
        Task<RegistroMaterialDto> SaveAsync(RegistroMaterialDto registroMaterialDto);
        
        Task<RegistroMaterialDto> UpdateAsync(RegistroMaterialDto registroMaterialDto);
        
        Task<List<RegistroMaterialDto>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<RegistroMaterialDto> GetByIdAsync(int id);
    }
}