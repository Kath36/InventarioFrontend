using Inventario.Core.Entities;

namespace Inventario.Repositories.Interfecies;

public interface IBrandRepository
{
    Task<Brand> SaveAsycn(Brand brand);
    
    Task<Brand> UpdateAsync(Brand brand);
    
    Task<List<Brand>> GetAllAsync();
    
    Task<bool> DeleteAsync(int id);
    
    Task<Brand> GetById(int id);
}