using Inventario.Core.Entities;

namespace Inventario.Api.Repositories.Interfecies
{
    public interface IDetallePedidoRepository
    {
        Task<DetallePedido> SaveAsync(DetallePedido detallePedido);
        
        Task<DetallePedido> UpdateAsync(DetallePedido detallePedido);
        
        Task<List<DetallePedido>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<DetallePedido> GetById(int id);
    }
}