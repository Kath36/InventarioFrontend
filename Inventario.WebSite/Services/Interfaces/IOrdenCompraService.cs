using Inventario.Api.Dto;
using Inventario.Core.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebSite.Services
{
    public interface IOrdenCompraService
    {
        Task<Response<List<OrdenCompraDto>>> GetAllAsync();

        Task<Response<OrdenCompraDto>> GetByIdAsync(int id);

        Task<Response<OrdenCompraDto>> SaveAsync(OrdenCompraDto ordenCompraDto);

        Task<Response<OrdenCompraDto>> UpdateAsync(OrdenCompraDto ordenCompraDto);

        Task<Response<bool>> DeleteAsync(int id);

        Task<Response<OrdenCompraDto>> GetByNameAsync(string name);
    }
}