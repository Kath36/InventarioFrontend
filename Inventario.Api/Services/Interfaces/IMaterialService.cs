using Inventario.Api.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.Services.Interfaces
{
    public interface IMaterialService
    {
        Task<bool> MaterialExists(int id);

        Task<MaterialDto> SaveAsync(MaterialDto material);

        Task<MaterialDto> UpdateAsync(MaterialDto material);

        Task<List<MaterialDto>> GetAllAsync();

        Task<bool> DeleteAsync(int id);

        Task<MaterialDto> GetByIdAsync(int id);
        Task<List<MaterialDto>> GetByNameAsync(string name);

    }
}

