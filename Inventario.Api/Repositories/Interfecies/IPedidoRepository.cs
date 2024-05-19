using Inventario.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Api.Repositories.Interfecies
{
    public interface IPedidoRepository
    {
        Task<Pedido> SaveAsync(Pedido pedido);
        
        Task<Pedido> UpdateAsync(Pedido pedido);
        
        Task<List<Pedido>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<Pedido> GetById(int id);
    }
}