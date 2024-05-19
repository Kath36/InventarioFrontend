using Dapper;
using Dapper.Contrib.Extensions;
using Inventario.Api.DataAccess.Interfaces;
using Inventario.Core.Entities;
using Inventario.Repositories.Interfecies;

namespace Inventario.Api.Repositories;

public class BrandRepository: IBrandRepository
{
    //Se prepara la clase para saber que se estara trabajando con una base de datos 
    private readonly IDbContext _dbContext;

    public BrandRepository(IDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Brand> SaveAsycn(Brand brand)
    {
        brand.id = await _dbContext.Connection.InsertAsync(brand);
        return brand;
    }

    public async Task<Brand> UpdateAsync(Brand brand)
    {
        await _dbContext.Connection.UpdateAsync(brand);
        return brand;
    }

    public async Task<List<Brand>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Brand WHERE isDeleted = 0";
        var brands = await _dbContext.Connection.QueryAsync<Brand>(sql);
        return brands.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var brand = await GetById(id);
        if (brand == null)
            return false;
        brand.IsDeleted = true;
        

        return await _dbContext.Connection.UpdateAsync(brand);
    }

    public async Task<Brand> GetById(int id)
    {
        var brand = await _dbContext.Connection.GetAsync<Brand>(id);
        if (brand == null)
            return null;
        return brand.IsDeleted == true ? null : brand;
    }
}