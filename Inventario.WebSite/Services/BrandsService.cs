using Newtonsoft.Json;
using Inventario.Core.Http;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Services;

public class BrandsService : IBrandsService
{
    public readonly string _baseURL = "http://localhost:5209/";
    private readonly string _endpoint = "api/Brand";

    public BrandsService()
    {
        
    }

    public async Task<Response<List<BrandDto>>> GetAllAsync()
    {
        var url = $"{_baseURL}{_endpoint}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<List<BrandDto>>>(json);
        
        return response;
    }
    public async Task<Response<BrandDto>> GetById(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<BrandDto>>(json);
        return response;
    }

    public async Task<Response<BrandDto>> SaveAsync(BrandDto brandDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(brandDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<BrandDto>>(json);

        return response;
    }
    public async Task<Response<BrandDto>> UpdateAsync(BrandDto brandDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(brandDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<BrandDto>>(json);

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";

        var client = new HttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        var responce = JsonConvert.DeserializeObject<Response<bool>>(json);
        return responce;
        
    }
}