using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Api.Dto;
using Inventario.Core.Http;

namespace Inventario.WebSite.Services
{
    public interface IPedidoService
    {
        Task<Response<List<PedidoDto>>> GetAllAsync();
        Task<Response<PedidoDto>> GetById(int id);
        Task<Response<PedidoDto>> SaveAsync(PedidoDto pedidoDto);
        Task<Response<PedidoDto>> UpdateAsync(PedidoDto pedidoDto);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<PedidoDto>> GetByNameAsync(string name);
    }
}