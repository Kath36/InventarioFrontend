using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Core.Entities;

namespace Inventario.Api.Repositories.Interfecies
{
    public interface IOrdenCompraRepository
    {
        Task<OrdenCompra> SaveAsync(OrdenCompra ordenCompra);
        
        Task<OrdenCompra> UpdateAsync(OrdenCompra ordenCompra);
        
        Task<List<OrdenCompra>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<OrdenCompra> GetById(int id);
    }
}



