using Inventario.Api.Dto;
using Inventario.Core.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventario.WebSite.Services
{
    public interface IUsuarioService
    {
        Task<Response<List<UsuarioDto>>> GetAllAsync();

        Task<Response<UsuarioDto>> GetById(int id);

        Task<Response<UsuarioDto>> SaveAsync(UsuarioDto usuarioDto);

        Task<Response<UsuarioDto>> UpdateAsync(UsuarioDto usuarioDto);

        Task<Response<bool>> DeleteAsync(int id);

        Task<Response<UsuarioDto>> GetByNameAsync(string name);
        Task<Response<UsuarioDto>> AuthenticateAsync(string email, string password);

    }
}