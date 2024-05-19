using Inventario.Core.Entities;

namespace Inventario.Api.Repositories.Interfecies
{
    public interface IMaterialRepository
    {
        Task<Material> SaveAsync(Material material);
        
        Task<Material> UpdateAsync(Material material);
        
        Task<List<Material>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<Material> GetById(int id);
        Task<List<Material>> GetByNameAsync(string name); // Agregar este método

    }
}

