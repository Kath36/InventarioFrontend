using Inventario.Api.Dto;

namespace Inventario.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<bool> PedidoExists(int id);
        
        Task<PedidoDto> SaveAsync(PedidoDto pedido);
        
        Task<PedidoDto> UpdateAsync(PedidoDto pedido);
        
        Task<List<PedidoDto>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<PedidoDto> GetById(int id);
    }
}