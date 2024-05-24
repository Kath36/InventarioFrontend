using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inventario.Api.Dto;
using Inventario.Core.Entities;
using Inventario.Services.Interfaces;
using Inventario.Core.Http;

namespace Inventario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosMaterialController : ControllerBase
    {
        private readonly IRegistroMaterialService _registroMaterialService;
        private readonly IMaterialService _materialService;

        public RegistrosMaterialController(IRegistroMaterialService registroMaterialService,
            IMaterialService materialService)
        {
            _registroMaterialService = registroMaterialService;
            _materialService = materialService;
        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////7777
        [HttpGet]
        public async Task<ActionResult<Response<List<RegistroMaterialDto>>>> GetAll()
        {
            var response = new Response<List<RegistroMaterialDto>>();

            try
            {
                response.Data = await _registroMaterialService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors.Add("Error al obtener los registros de material: ");
                return StatusCode(500, response);
            }
        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////7777

        [HttpPost]
        public async Task<ActionResult<Response<RegistroMaterialDto>>> Post(
            [FromBody] RegistroMaterialDtoSinId registroMaterialDto)
        {
            var response = new Response<RegistroMaterialDto>();
            var validationErrors = new List<string>();

            if (registroMaterialDto.MaterialId <= 0)
            {
                validationErrors.Add("El campo MaterialId es obligatorio.");
            }

            if (registroMaterialDto.Cantidad <= 0)
            {
                validationErrors.Add("El campo Cantidad debe ser mayor que cero.");
            }

            if (!await _materialService.MaterialExists(registroMaterialDto.MaterialId))
            {
                validationErrors.Add("El MaterialId no existe.");
            }

            if (validationErrors.Any())
            {
                response.Errors.AddRange(validationErrors);
                return BadRequest(response);
            }

            var registroMaterialDtoWithId = new RegistroMaterialDto
            {
                MaterialId = registroMaterialDto.MaterialId,
                Cantidad = registroMaterialDto.Cantidad,
                Fecha_Registro = DateTime.Now
            };

            response.Data = await _registroMaterialService.SaveAsync(registroMaterialDtoWithId);
            return Created($"/api/[controller]/{response.Data.id}", response);
        }



        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////7777
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<RegistroMaterialDto>>> GetById(int id)
        {
            var response = new Response<RegistroMaterialDto>();

            if (!await _registroMaterialService.RegistroMaterialExists(id))
            {
                response.Errors.Add("Registro de material no encontrado");
                return NotFound(response);
            }

            try
            {
                response.Data = await _registroMaterialService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors.Add("Registro de material no encontrado");
                return StatusCode(500, response);
            }
        }

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////7777
        [HttpPut("{id}")]
        public async Task<ActionResult<Response<RegistroMaterialDto>>> UpdateAsync(int id, RegistroMaterialDto registroMaterialDto)
        {
            var response = new Response<RegistroMaterialDto>();
            var validationErrors = new List<string>();

            // Validaciones
            if (registroMaterialDto.MaterialId <= 0)
            {
                validationErrors.Add("El campo MaterialId es obligatorio.");
            }

            if (registroMaterialDto.Cantidad <= 0)
            {
                validationErrors.Add("El campo Cantidad debe ser mayor que cero.");
            }

            if (!await _materialService.MaterialExists(registroMaterialDto.MaterialId))
            {
                validationErrors.Add("El MaterialId no existe.");
            }

            if (validationErrors.Any())
            {
                response.Errors.AddRange(validationErrors);
                return BadRequest(response);
            }

            // Verifica si el registro de material existe
            if (!await _registroMaterialService.RegistroMaterialExists(id))
            {
                response.Errors.Add("Registro de material no encontrado");
                return NotFound(response);
            }

            try
            {
                // Actualiza el registro de material
                registroMaterialDto.id = id;
                response.Data = await _registroMaterialService.UpdateAsync(registroMaterialDto);
                response.Message = "El registro de material se actualizó correctamente.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors.Add("Ocurrió un error al procesar la solicitud: " + ex.Message);
                return StatusCode(500, response);
            }
        }


    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////7777
    [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<bool>>> Delete(int id)
        {
            var response = new Response<bool>();

            if (!await _registroMaterialService.DeleteAsync(id))
            {
                response.Errors.Add("Registro de material no encontrado");
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////7777