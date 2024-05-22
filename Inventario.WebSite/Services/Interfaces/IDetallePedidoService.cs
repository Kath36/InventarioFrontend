using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Api.Dto;
using Inventario.Core.Http;

namespace Inventario.WebSite.Services
{
    public interface IDetallePedidoService
    {
        Task<Response<List<DetallePedidoDto>>> GetAllAsync();
        Task<Response<DetallePedidoDto>> GetById(int id);
        Task<Response<DetallePedidoDto>> SaveAsync(DetallePedidoDto detallePedidoDto);
        Task<Response<DetallePedidoDto>> UpdateAsync(DetallePedidoDto detallePedidoDto);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<DetallePedidoDto>> GetByMaterialIdAsync(int materialId);
    }
}