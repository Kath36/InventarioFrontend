using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Inventario.Api.Dto;
using Newtonsoft.Json;
using System.Collections.Generic;
using Inventario.Core.Http;

namespace Inventario.WebSite.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly string _baseURL = "http://localhost:5209/";
        private readonly string _endpoint = "api/Proveedores";

        public ProveedorService()
        {
            
        }

        public async Task<Response<List<ProveedorDto>>> GetAllAsync()
        {
            var url = $"{_baseURL}{_endpoint}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<List<ProveedorDto>>>(jsonResponse);
        }

        public async Task<Response<ProveedorDto>> GetById(int id)
        {
            var url = $"{_baseURL}{_endpoint}/{id}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ProveedorDto>>(jsonResponse);
        }

        public async Task<Response<ProveedorDto>> SaveAsync(ProveedorDto proveedorDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            using var client = new HttpClient();
            var jsonContent = JsonConvert.SerializeObject(proveedorDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ProveedorDto>>(jsonResponse);
        }

        public async Task<Response<ProveedorDto>> UpdateAsync(ProveedorDto proveedorDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            using var client = new HttpClient();
            var jsonContent = JsonConvert.SerializeObject(proveedorDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ProveedorDto>>(jsonResponse);
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var url = $"{_baseURL}{_endpoint}/{id}";
            using var client = new HttpClient();
            var response = await client.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<bool>>(jsonResponse);
        }

        public async Task<Response<ProveedorDto>> GetByNameAsync(string name)
        {
            var url = $"{_baseURL}{_endpoint}/nombre/{name}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<ProveedorDto>>(jsonResponse);
        }
    }
}
