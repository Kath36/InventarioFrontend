using Inventario.Api.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Services.Interfaces
{
    public interface IOrdenCompraService
    {
        Task<bool> OrdenCompraExists(int id);
        
        Task<OrdenCompraDto> SaveAsync(OrdenCompraDto ordenCompra);
        
        Task<OrdenCompraDto> UpdateAsync(OrdenCompraDto ordenCompra);
        
        Task<List<OrdenCompraDto>> GetAllAsync();
        
        Task<bool> DeleteAsync(int id);
        
        Task<OrdenCompraDto> GetById(int id);
    }
}