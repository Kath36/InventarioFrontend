using Newtonsoft.Json;
using Inventario.Core.Http;
using Inventario.Api.Dto;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Inventario.WebSite.Services
{
    public class RegistroMaterialService : IRegistroMaterialService
    {
        private readonly string _baseURL = "http://localhost:5209/";
        private readonly string _endpoint = "api/RegistrosMaterial";

        public RegistroMaterialService() {}

        public async Task<Response<List<RegistroMaterialDto>>> GetAllAsync()
        {
            var url = $"{_baseURL}{_endpoint}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<List<RegistroMaterialDto>>>(jsonResponse);
        }

        public async Task<Response<RegistroMaterialDto>> GetById(int id)
        {
            var url = $"{_baseURL}{_endpoint}/{id}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<RegistroMaterialDto>>(jsonResponse);
        }

        public async Task<Response<RegistroMaterialDto>> SaveAsync(RegistroMaterialDto registroMaterialDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(registroMaterialDto);
            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<RegistroMaterialDto>>(jsonResponse);
        }

        public async Task<Response<RegistroMaterialDto>> UpdateAsync(RegistroMaterialDto registroMaterialDto)
        {
            var url = $"{_baseURL}{_endpoint}/{registroMaterialDto.id}";
            var jsonRequest = JsonConvert.SerializeObject(registroMaterialDto);
            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<RegistroMaterialDto>>(jsonResponse);
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

        public async Task<Response<RegistroMaterialDto>> GetByNameAsync(string nombre)
        {
            var url = $"{_baseURL}{_endpoint}/nombre/{nombre}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<RegistroMaterialDto>>(jsonResponse);
        }

        public async Task<bool> RegistroMaterialExists(int id)
        {
            var registroMaterial = await GetById(id);
            return registroMaterial != null;
        }
    }
}
