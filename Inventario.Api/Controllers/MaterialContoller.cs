using Microsoft.AspNetCore.Mvc;
using Inventario.Api.Dto;
using Inventario.Services.Interfaces;
using Inventario.Core.Http;


namespace Inventario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialesController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialesController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

// OPTENER TODOOOO //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public async Task<ActionResult<Response<List<MaterialDto>>>> GetAll()
        {
            try
            {
                var response = new Response<List<MaterialDto>>
                {
                    Data = await _materialService.GetAllAsync()
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "No hay datos que mostrar " });
            }
        }
// AGREGAR //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost]
        public async Task<ActionResult<Response<MaterialDto>>> Post([FromBody] MaterialDtoSinId materialDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = new Response<MaterialDto>();

                if (string.IsNullOrEmpty(materialDto.Nombre))
                {
                    ModelState.AddModelError(nameof(materialDto.Nombre), "El nombre del material es obligatorio.");
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(materialDto.Descripcion))
                {
                    ModelState.AddModelError(nameof(materialDto.Descripcion),
                        "La descripción del material es obligatoria.");
                    return BadRequest(ModelState);
                }

                if (materialDto.Precio <= 0)
                {
                    ModelState.AddModelError(nameof(materialDto.Precio),
                        "El precio del material debe ser mayor que cero.");
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(materialDto.Unidad))
                {
                    ModelState.AddModelError(nameof(materialDto.Unidad), "La unidad del material es obligatoria.");
                    return BadRequest(ModelState);
                }

                var materialDtoWithId = new MaterialDto
                {
                    Nombre = materialDto.Nombre,
                    Descripcion = materialDto.Descripcion,
                    Precio = materialDto.Precio,
                    Unidad = materialDto.Unidad
                };
                response.Data = await _materialService.SaveAsync(materialDtoWithId);

                response.Message = "El material se agregó exitosamente.";

                return Created($"/api/[controller]/{response.Data.id}", response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en el método Post: {ex}");
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud." });
            }
        }


// OBTENER POR ID //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<MaterialDto>>> GetById(int id)
        {
            try
            {
                var response = new Response<MaterialDto>();

                if (!await _materialService.MaterialExists(id))
                {
                    response.Errors.Add("No existe el ID ingresado");
                    return NotFound(response);
                }

                response.Data = await _materialService.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "No existe el ID ingresado" });
            }
        }


// ACTUALIZAR  //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPut]
        public async Task<ActionResult<Response<MaterialDto>>> Update([FromBody] MaterialDto materialDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = new Response<MaterialDto>();

                if (!await _materialService.MaterialExists(materialDto.id))
                {
                    response.Errors.Add("No existe el ID ingresado");
                    return NotFound(response);
                }

                response.Data = await _materialService.UpdateAsync(materialDto);

                response.Message = "Material actualizado correctamente";

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "No existe el ID ingresado" });
            }
        }
// ELIMINAR //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Response<bool>>> Delete(int id)
        {
            try
            {
                var response = new Response<bool>();
                if (!await _materialService.DeleteAsync(id))
                {
                    response.Errors.Add("Material no se pudo eliminar, verifica si el ID existe");
                    return NotFound(response);
                }

                response.Message = "Material eliminado correctamente";
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "No existe el ID ingresado" + ex.Message });
            }
        }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        [HttpGet]
        [Route("nombre/{nombre}")]

        public async Task<ActionResult<Response<List<MaterialDto>>>> GetByName(string nombre)
        {
            try
            {
                var response = new Response<List<MaterialDto>>();

                var materials = await _materialService.GetByNameAsync(nombre);
                if (materials == null || !materials.Any())
                {
                    response.Errors.Add("No se encontraron materiales con el nombre especificado.");
                    return NotFound(response);
                }

                response.Data = materials;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener materiales por nombre." });
            }
        }




    }
}