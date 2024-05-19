using Dapper;
using Dapper.Contrib.Extensions;
using Inventario.Api.DataAccess.Interfaces;
using Inventario.Api.Repositories.Interfecies;
using Inventario.Core.Entities;

namespace Inventario.Api.Repositories;

public class ProductCategoryRepositoty : IProductCategoryRepository
{
    //preparar la clase para saber que estaremos trabajando con una base de datos
    private readonly IDbContext _dbContext;

    public ProductCategoryRepositoty(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<ProductCategory> SaveAsycn(ProductCategory category)
    {
        category.id = await _dbContext.Connection.InsertAsync(category);
        return category;
    }

    public async Task<ProductCategory> UpdateAsync(ProductCategory category)
    {
        await _dbContext.Connection.UpdateAsync(category);
        return category;
    }

    public async Task<List<ProductCategory>> GetAllAsync()
    {
        const string sql = "SELECT * FROM ProductCategory WHERE isDeleted = 0";
        var categories = await _dbContext.Connection.QueryAsync<ProductCategory>(sql);
        return categories.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await GetById(id);
        if (category == null)
            return false;
            category.IsDeleted = true;
        

        return await _dbContext.Connection.UpdateAsync(category);
    }

    public async Task<ProductCategory> GetById(int id)
    {
        var category = await _dbContext.Connection.GetAsync<ProductCategory>(id);
        if (category == null)
            return null;
            return category.IsDeleted == true ? null : category;
    }

    public async Task<ProductCategory> GetByName(string name, int id = 0)
    {
        string sql = $"SELECT * FROM  ProductCategory WHERE Name = '{name}' AND id <> {id}";
        var categories = await _dbContext.Connection.QueryAsync<ProductCategory>(sql);

        return categories.ToList().FirstOrDefault();
    }

}