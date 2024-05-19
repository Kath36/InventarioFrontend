using Inventario.Api.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Inventario.Core.Entities;
using Inventario.Core.Http;
using Inventario.Api.Dto;
using Inventario.Api.Repositories.Interfecies;
using Inventario.Api.Services;
using Inventario.Api.Services.Interfaces;

namespace Tecnm.Ecommerce1.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;
    
    
    public BrandController( IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<Brand>>>> GetAll()
    {
        var response = new Response<List<BrandDto>>
        {
            Data = await _brandService.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<Brand>>> Post([FromBody] BrandDto brandDto)
    {
        var response = new Response<BrandDto>()
        {
            Data = await _brandService.SaveAsycn(brandDto)
        };
        
        return Created($"/api/[controller]/{response.Data.id}", response);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<BrandDto>>> GetbyId(int id)
    {
        var response = new Response<BrandDto>();
//Se evalua si el valor fue encontrado o no, condicion si existen valores nulos
        if (!await _brandService.BrandExist(id))
        {
            response.Errors.Add("brand not found");
            return NotFound(response);
        }

        response.Data = await _brandService.GetById(id);
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<Brand>>> Update([FromBody] BrandDto brandDto)
    {
        var response = new Response<BrandDto>();
        if (!await _brandService.BrandExist(brandDto.id))
        {
            response.Errors.Add("Brand not found");
            return NotFound(response);
        }

        response.Data = await _brandService.UpdateAsync(brandDto);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async  Task<ActionResult<Response<bool>>> DeleteAsync(int id)
    {
        var category = await _brandService.DeleteAsync(id);
        var response = new Response<bool>();
        response.Data = category;
//Se evalua si el valor fue encontrado o no, condicion si existen valores nulos
        if (category == null)
        {
            response.Errors.Add("brand not found");
            return NotFound(response);
        }

        response.Message = ("brand found it!");
        return Ok(response);
    }

}