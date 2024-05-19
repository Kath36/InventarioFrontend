using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Inventario.Core.Entities;
using Inventario.Api.Dto;
using Inventario.Api.Repositories.Interfecies;
using Inventario.Services.Interfaces;
using Inventario.Core.Http;

namespace Inventario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        public async Task<ActionResult<Response<List<PedidoDto>>>> GetAll()
        {
            var response = new Response<List<PedidoDto>>
            {
                Data = await _pedidoService.GetAllAsync()
            };
            return Ok(response);
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        public async Task<ActionResult<Response<PedidoDto>>> Post([FromBody] PedidoDtoSinId pedidoDto)
        {
            try
            {
                if (string.IsNullOrEmpty(pedidoDto.Cliente) || string.IsNullOrEmpty(pedidoDto.Estado))
                {
                    return BadRequest(
                        new { message = "Los campos de cliente y estado son obligatorios" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = new Response<PedidoDto>();
                var pedidoDtoWithId = new PedidoDto
                {
                    Cliente = pedidoDto.Cliente,
                    Fecha_Pedido = DateTime.Now,
                    Estado = pedidoDto.Estado
                };
                response.Data = await _pedidoService.SaveAsync(pedidoDtoWithId);
                response.Message = "Pedido agregado correctamente";

                return Created($"/api/[controller]/{response.Data.id}", response);
            }
            catch (Exception ex)
            { 
                Console.WriteLine($"Error en el método Post: {ex}");
                return StatusCode(500,
                    new { message = "Tienes que colocar el cliente correcto y/o el Estado en que esta ese pedido." });
            }
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<PedidoDto>>> GetById(int id)
        {
            var response = new Response<PedidoDto>();
            if (!await _pedidoService.PedidoExists(id))
            {
                response.Errors.Add("No existe");
                return NotFound(response);
            }
            response.Data = await _pedidoService.GetById(id);
            return response;
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        public async Task<ActionResult<Response<PedidoDto>>> Update([FromBody] PedidoDto pedidoDto)
        {
            var response = new Response<PedidoDto>();

            if (!await _pedidoService.PedidoExists(pedidoDto.id))
            {
                response.Errors.Add("No existe");
                return NotFound(response);
            }
            var pedidoDtoToUpdate = new PedidoDto
            {
                id = pedidoDto.id,
                Cliente = pedidoDto.Cliente,
                Estado = pedidoDto.Estado
            };
            response.Data = await _pedidoService.UpdateAsync(pedidoDtoToUpdate);
            response.Message = "El pedido se actualizó correctamente";

            return Ok(response);
        }
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<bool>>> Delete(int id)
        {
            var response = new Response<bool>();

            if (!await _pedidoService.DeleteAsync(id))
            {
                response.Errors.Add("El ID ingresado no existe");
                return NotFound(response);
            }
            response.Message = "El pedido se eliminó correctamente";

            return Ok(response);
        }
    }
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
