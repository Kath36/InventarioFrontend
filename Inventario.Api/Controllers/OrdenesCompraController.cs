using Inventario.Api.Dto;
using Inventario.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inventario.Core.Http;

namespace Inventario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenCompraController : ControllerBase
    {
        private readonly IOrdenCompraService _ordenCompraService;
        private readonly IMaterialService _materialService;
        private readonly IProveedorService _proveedorService;

        public OrdenCompraController(IOrdenCompraService ordenCompraService, IMaterialService materialService, IProveedorService proveedorService)
        {
            _ordenCompraService = ordenCompraService;
            _materialService = materialService;
            _proveedorService = proveedorService;
        }


//e////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<ActionResult<Response<List<OrdenCompraDto>>>> GetAll()
        {
            try
            {
                var response = new Response<List<OrdenCompraDto>>
                {
                    Data = await _ordenCompraService.GetAllAsync()
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Noy hay datos" });
            }
        }

//e///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
        [HttpPost]
        public async Task<ActionResult<Response<OrdenCompraDto>>> Post([FromBody] OrdenCompraDto ordenCompraDto)
        {
            var response = new Response<OrdenCompraDto>();

            var validationErrors = new List<string>();

            if (ordenCompraDto.MaterialId <= 0)
            {
                validationErrors.Add("El campo MaterialId es obligatorio.");
            }

            if (ordenCompraDto.ProveedorId <= 0)
            {
                validationErrors.Add("El campo ProveedorId es obligatorio.");
            }

            if (ordenCompraDto.Cantidad <= 0)
            {
                validationErrors.Add("El campo Cantidad debe ser mayor que cero.");
            }

            if (!await _materialService.MaterialExists(ordenCompraDto.MaterialId))
            {
                validationErrors.Add("El MaterialId no existe.");
            }

            if (!await _proveedorService.ProveedorExists(ordenCompraDto.ProveedorId))
            {
                validationErrors.Add("El ProveedorId no existe.");
            }

            if (validationErrors.Any())
            {
                response.Errors.AddRange(validationErrors);
                return BadRequest(response);
            }

            OrdenCompraDto ordenCompraDtoWithId = new OrdenCompraDto
            {
                MaterialId = ordenCompraDto.MaterialId,
                ProveedorId = ordenCompraDto.ProveedorId,
                Cantidad = ordenCompraDto.Cantidad,
                FechaOrden = DateTime.Now
            };

            response.Data = await _ordenCompraService.SaveAsync(ordenCompraDtoWithId);
            return Created($"/api/[controller]/{response.Data.id}", response);
        }

//e/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<OrdenCompraDto>>> GetById(int id)
        {
            try
            {
                var response = new Response<OrdenCompraDto>();

                if (!await _ordenCompraService.OrdenCompraExists(id))
                {
                    return StatusCode(500, new { message = "El ID ingresado no existe" });
                    return NotFound(response);
                }

                response.Data = await _ordenCompraService.GetById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "El ID ingresado no existe" });
            }
        }

//e///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        


        [HttpPut]
        public async Task<ActionResult<Response<OrdenCompraDto>>> Update([FromBody] OrdenCompraDto ordenCompraDto)
        {
            var response = new Response<OrdenCompraDto>();

            try
            {
                if (!await _ordenCompraService.OrdenCompraExists(ordenCompraDto.id))
                {
                    response.Errors.Add("Orden de compra no encontrada");
                    return NotFound(response);
                }
                if (ordenCompraDto.Cantidad == 0)
                {
                    ModelState.AddModelError(nameof(ordenCompraDto.Cantidad), "La Cantidad es obligatoria.");
                    return BadRequest(ModelState);
                }
                var ordenCompraDtoToUpdate = new OrdenCompraDto
                {
                    id = ordenCompraDto.id,
                    MaterialId = ordenCompraDto.MaterialId,
                    Cantidad = ordenCompraDto.Cantidad,
                    ProveedorId = ordenCompraDto.ProveedorId
                };
                response.Data = await _ordenCompraService.UpdateAsync(ordenCompraDtoToUpdate);
                response.Message = "La orden de compra se actualizó correctamente";

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método Update: {ex}");
                return StatusCode(500, new { message = "Verifica que    el ID material y/o ID proveedor existan." });
            }
        }

//e///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<bool>>> Delete(int id)
        {
            try
            {
                var response = new Response<bool>();

                if (!await _ordenCompraService.DeleteAsync(id))
                {
                    return NotFound(new { message = "El ID ingresado no existe" });
                }
                response.Message = "El elemento fue eliminado correctamente";

                return Ok(response);
            }
            catch (Exception ex)
            { Console.WriteLine($"Error en el método Delete: {ex}");

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud." });
            }
        }
    }
}
//e///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////        
