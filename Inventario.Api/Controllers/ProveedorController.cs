﻿using System.Collections.Generic;
using System.Threading.Tasks;
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
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedoresController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }
//U////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<ActionResult<Response<List<ProveedorDto>>>> GetAll()
        {
            var response = new Response<List<ProveedorDto>>();

            try
            {
                response.Data = await _proveedorService.GetAllAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Errors.Add("No hay datos para mostrar");
                return StatusCode(500, response);
            }
        }
//UU//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////7        
     [HttpPost]
public async Task<ActionResult<Response<ProveedorDto>>> Post([FromBody] ProveedorDtoSinId proveedorDto)
{
    try
    {
        if (!ModelState.IsValid)
        {
            var validationErrors = new List<string>();

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                validationErrors.Add(error.ErrorMessage);
            }
            return BadRequest(new { errors = validationErrors });
        }
        var response = new Response<ProveedorDto>();

        if (string.IsNullOrEmpty(proveedorDto.Nombre))
        {
            response.Errors.Add("El nombre del proveedor es obligatorio.");
        }
        if (string.IsNullOrEmpty(proveedorDto.Direccion))
        {
            response.Errors.Add("La dirección del proveedor es obligatoria.");
        }

        if (string.IsNullOrEmpty(proveedorDto.Telefono))
        {
            response.Errors.Add("El teléfono del proveedor es obligatorio.");
        }

        if (response.Errors.Any())
        {
            return BadRequest(new { errors = response.Errors });
        }  
        var proveedorDtoWithId = new ProveedorDto
        {
            Nombre = proveedorDto.Nombre,
            Direccion = proveedorDto.Direccion,
            Telefono = proveedorDto.Telefono
        };

        response.Data = await _proveedorService.SaveAsync(proveedorDtoWithId);

        response.Message = "El proveedor se ha agregado correctamente.";

        return Created($"/api/[controller]/{response.Data.id}", response);
    }
    catch (Exception ex)
    {
        var errorList = new List<string>();
        errorList.Add($"Error en el método Post: {ex.Message}");

        return StatusCode(500, new { errors = errorList });
    }
}

//U////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<ProveedorDto>>> GetById(int id)
        {
            var response = new Response<ProveedorDto>();

            if (!await _proveedorService.ProveedorExists(id))
            {
                response.Errors.Add("No existe el ID ingresado verifiquelo");
                return NotFound(response);
            }

            response.Data = await _proveedorService.GetByIdAsync(id);
            return Ok(response);
        }
//U////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpPut]
        public async Task<ActionResult<Response<ProveedorDto>>> Update([FromBody] ProveedorDto proveedorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = new Response<ProveedorDto>();

                if (!await _proveedorService.ProveedorExists(proveedorDto.id))
                {
                    response.Errors.Add("No existe el ID ingresado. Verifíquelo.");
                    return NotFound(response);
                }

                response.Data = await _proveedorService.UpdateAsync(proveedorDto);

                // Agregar un mensaje de éxito a la respuesta
                response.Message = "El proveedor se ha actualizado correctamente.";

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "No se pudo actualizar el proveedor. Error interno del servidor: " + ex.Message });
            }
        }

//U////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<bool>>> Delete(int id)
        {
            var response = new Response<bool>();

            if (!await _proveedorService.DeleteAsync(id))
            {
                response.Errors.Add("El ID ingresado no existe");
                return NotFound(response);
            }
            response.Message = "El Proveedor se eliminó correctamente";
            return Ok(response);
        }
    }
}
