using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Inventario.Core.Entities;
using Inventario.Core.Http;
using Inventario.Api.Dto;
using Inventario.Api.Repositories.Interfecies;
using Inventario.Api.Services;
using Inventario.Api.Services.Interfaces;

namespace Inventario.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ProductCategoriesController : ControllerBase
{
    private readonly IProductCategoryService _productCategoryService;
    
    
    public ProductCategoriesController( IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<ProductCategory>>>> GetAll()
    {
        var response = new Response<List<ProductCategoryDto>>
        {
            Data = await _productCategoryService.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ProductCategory>>> Post([FromBody] ProductCategoryDto categoryDto)
    {
        var response = new Response<ProductCategoryDto>();

        if (await _productCategoryService.ExistByName(categoryDto.Name))
        {
            response.Errors.Add($"Product Category name {categoryDto.Name} already exists");
            return BadRequest(response);
        }

        response.Data = await _productCategoryService.SaveAsycn(categoryDto);
        
        return Created($"/api/[controller]/{response.Data.id}", response);
    }
    

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<Response<ProductCategoryDto>>> GetbyId(int id)
    {
        var response = new Response<ProductCategoryDto>();
        if (!await _productCategoryService.ProductCategoryExist(id))
        {
            response.Errors.Add("category not found");
            return NotFound(response);
        }

        response.Data = await _productCategoryService.GetById(id);
        return Ok(response);
    }


    [HttpPut]
    public async Task<ActionResult<Response<ProductCategory>>> Update([FromBody] ProductCategoryDto categoryDto)
    {
        var response = new Response<ProductCategoryDto>();
        if (!await _productCategoryService.ProductCategoryExist(categoryDto.id))
        {
            response.Errors.Add("Product category not found");
            return NotFound(response);
        }

        if (await _productCategoryService.ExistByName(categoryDto.Name, categoryDto.id))
        {
            response.Errors.Add($"Product category NAme {categoryDto.Name} already exist");
            return BadRequest(response);
        }

        response.Data = await _productCategoryService.UpdateAsync(categoryDto);
        return Ok(response);
    }
    
    [HttpDelete]
    [Route("{id:int}")]
    public async  Task<ActionResult<Response<bool>>> DeleteAsync(int id)
    {
        var category = await _productCategoryService.DeleteAsync(id);
        var response = new Response<bool>();
        response.Data = category;
//Se evalua si el valor fue encontrado o no, condicion si existen valores nulos
        if (category == null)
        {
            response.Errors.Add("category not found");
            return NotFound(response);
        }

        response.Message = ("Category found it!");
        return Ok(response);
    }
}