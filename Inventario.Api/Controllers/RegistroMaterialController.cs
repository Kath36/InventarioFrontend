using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inventario.Api.Dto;
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

        public RegistrosMaterialController(IRegistroMaterialService registroMaterialService, IMaterialService materialService)
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
public async Task<ActionResult<Response<RegistroMaterialDtoSinId>>> Post([FromBody] RegistroMaterialDtoSinId registroMaterialDto)
{
    try
    {
        if (!ModelState.IsValid)
        {
            var validationErrors = new List<string>();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    validationErrors.Add(error.ErrorMessage);
                }
            }
            return BadRequest(new { errors = validationErrors });
        }
        if (registroMaterialDto.MaterialId <= 0)
        {
            ModelState.AddModelError(nameof(registroMaterialDto.MaterialId), "El MaterialId es obligatorio.");
        }
        if (registroMaterialDto.Cantidad <= 0)
        {
            ModelState.AddModelError(nameof(registroMaterialDto.Cantidad), "La Cantidad debe ser mayor que cero.");
        }
        if (!await _materialService.MaterialExists(registroMaterialDto.MaterialId))
        {
            ModelState.AddModelError(nameof(registroMaterialDto.MaterialId), "El MaterialId no existe.");
        }
        if (!ModelState.IsValid)
        { var validationErrors = new List<string>();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    validationErrors.Add(error.ErrorMessage);
                }
            }
            return BadRequest(new { errors = validationErrors });
        }
        var registroMaterialDtoWithId = new RegistroMaterialDto
        {
            MaterialId = registroMaterialDto.MaterialId,
            Cantidad = registroMaterialDto.Cantidad,
            Fecha_Registro = DateTime.Now 
        };
        await _registroMaterialService.SaveAsync(registroMaterialDtoWithId);
        return Ok(new { message = "El registro de material se agregó correctamente." });
    }
    catch (Exception ex)
    { 
        return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud: " + ex.Message });
    }
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
[HttpPut]
public async Task<ActionResult<Response<RegistroMaterialDto>>> Update([FromBody] RegistroMaterialDto registroMaterialDto)
{
    try
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await _registroMaterialService.RegistroMaterialExists(registroMaterialDto.id))
        {
            var response = new Response<RegistroMaterialDto>();
            response.Errors.Add("Registro de material no encontrado");
            return NotFound(response);
        }
        if (!await _materialService.MaterialExists(registroMaterialDto.MaterialId))
        { 
            var response = new Response<RegistroMaterialDto>();
            response.Errors.Add("El MaterialId no existe.");
            return BadRequest(response);
        }
        if (registroMaterialDto.Cantidad <= 0)
        {
            var response = new Response<RegistroMaterialDto>();
            response.Errors.Add("La Cantidad debe ser mayor que cero.");
            return BadRequest(response);
        }
        var updatedRegistroMaterial = await _registroMaterialService.UpdateAsync(registroMaterialDto);
        var okResponse = new Response<RegistroMaterialDto>();
        okResponse.Data = updatedRegistroMaterial;
        okResponse.Message = "El registro de material se actualizó correctamente.";
        return Ok(okResponse);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error en el método Update: {ex}");
        return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud." });
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