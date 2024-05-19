using Inventario.Api.Dto;

namespace Inventario.Services.Interfaces
{
    public interface IDetallePedidoService
    {
        Task<bool> DetallePedidoExists(int id);
        
        Task<DetallePedidoDto> SaveAsync(DetallePedidoDto detallePedido);
        
        Task<DetallePedidoDto> UpdateAsync(DetallePedidoDto detallePedido);
        
        Task<List<DetallePedidoDto>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<DetallePedidoDto> GetById(int id);
    }
}