using Microsoft.AspNetCore.Mvc;
using Inventario.Api.Dto;
using Inventario.Services.Interfaces;
using System.Threading.Tasks;
using Inventario.Core.Http;

namespace Inventario.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public AuthController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("authenticate")]
        public async Task<ActionResult<Response<UsuarioDto>>> Authenticate([FromBody] LoginRequestDto loginRequest)
        {
            try
            {
                if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) ||
                    string.IsNullOrEmpty(loginRequest.Contraseña))
                {
                    return BadRequest(new Response<UsuarioDto>
                    {
                        Success = false,
                        Message = "Los campos de correo electrónico y contraseña son obligatorios."
                    });
                }

                var usuario = await _usuarioService.AuthenticateAsync(loginRequest.Email, loginRequest.Contraseña);

                if (usuario == null)
                {
                    return BadRequest(new Response<UsuarioDto>
                    {
                        Success = false,
                        Message = "Correo electrónico o contraseña incorrectos."
                    });
                }

                return Ok(new Response<UsuarioDto>
                {
                    Success = true,
                    Data = usuario,
                    Message = "Autenticación exitosa."
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método Authenticate: {ex}");
                return StatusCode(500, new Response<UsuarioDto>
                {
                    Success = false,
                    Message = "Ocurrió un error al procesar la solicitud."
                });
            }
        }

    
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<ActionResult<Response<List<UsuarioDto>>>> GetAll()
        {
            var response = new Response<List<UsuarioDto>>
            {
                Data = await _usuarioService.GetAllUsuariosAsync()
            };
            return Ok(response);
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [HttpPost]
        public async Task<ActionResult<Response<UsuarioDto>>> Post([FromBody] UsuarioDtoSinId usuarioDto)
        {
            try
            {
                // Verificar si los campos no son nulos
                if (usuarioDto == null || string.IsNullOrEmpty(usuarioDto.Email) ||
                    string.IsNullOrEmpty(usuarioDto.Contraseña))
                {
                    return BadRequest(
                        new { message = "Los campos de correo electrónico y contraseña son obligatorios" });
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = new Response<UsuarioDto>();
                var usuarioDtoWithId = new UsuarioDto
                {
                    Email = usuarioDto.Email,
                    Contraseña = usuarioDto.Contraseña
                };
                response.Data = await _usuarioService.RegistrarUsuarioAsync(usuarioDtoWithId);
                response.Message = "El usuario se agregó correctamente";
                return Created($"/api/[controller]/{response.Data.id}", response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método Post: {ex}");
                return StatusCode(500, new { message = "Por seguridad, crea un correo y una contraseña" });
            }
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<UsuarioDto>>> GetById(int id)
        {
            var response = new Response<UsuarioDto>();
            if (!await _usuarioService.UsuarioExists(id))
            {
                response.Errors.Add("Usuario no encontrado");
                return NotFound(response);
            }

            response.Data = await _usuarioService.GetUsuarioByIdAsync(id);
            return Ok(response);
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        public async Task<ActionResult<Response<UsuarioDto>>> Update([FromBody] UsuarioDto usuarioDto)
        {
            var response = new Response<UsuarioDto>();
            try
            {
                if (!await _usuarioService.UsuarioExists(usuarioDto.id))
                {
                    response.Errors.Add("Usuario no encontrado");
                    return NotFound(response);
                }

                response.Data = await _usuarioService.ActualizarUsuarioAsync(usuarioDto);
                response.Message = "El usuario se actualizó correctamente";

                return Ok(response);
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error en el método Update: {ex}");
                return StatusCode(500, new { message = "Este correo ya existe, ingrese otro" });
            }
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<bool>>> Delete(int id)
        {
            var response = new Response<bool>();
            if (!await _usuarioService.UsuarioExists(id))
            {
                response.Errors.Add("Usuario no encontrado");
                return NotFound(response);
            }
            response.Data = await _usuarioService.EliminarUsuarioAsync(id);
            response.Message = "Usuario eliminado correctamente";
            return Ok(response);
        }
    }
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
