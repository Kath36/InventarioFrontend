using Inventario.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Api.Repositories.Interfecies
{
    public interface IProveedorRepository
    {
        Task<Proveedor> SaveAsync(Proveedor proveedor);
        
        Task<Proveedor> UpdateAsync(Proveedor proveedor);
        
        Task<List<Proveedor>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<Proveedor> GetById(int id);
    }
}






