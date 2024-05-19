using Inventario.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Api.Repositories.Interfecies
{
    public interface IRegistroMaterialRepository
    {
        Task<RegistroMaterial> SaveAsync(RegistroMaterial registroMaterial);
        
        Task<RegistroMaterial> UpdateAsync(RegistroMaterial registroMaterial);
        
        Task<List<RegistroMaterial>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<RegistroMaterial> GetById(int id);
    }
}