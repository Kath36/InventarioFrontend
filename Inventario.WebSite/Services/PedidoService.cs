using Newtonsoft.Json;
using Inventario.Core.Http;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly string _baseURL = "http://localhost:5209/";
        private readonly string _endpoint = "api/Pedidos";

        public PedidoService()
        {

        }

        public async Task<Response<List<PedidoDto>>> GetAllAsync()
        {
            var url = $"{_baseURL}{_endpoint}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<List<PedidoDto>>>(jsonResponse);
        }

        public async Task<Response<PedidoDto>> GetById(int id)
        {
            var url = $"{_baseURL}{_endpoint}/{id}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<PedidoDto>>(jsonResponse);
        }

        public async Task<Response<PedidoDto>> SaveAsync(PedidoDto pedidoDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(pedidoDto);
            using var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<PedidoDto>>(jsonResponse);
        }

        public async Task<Response<PedidoDto>> UpdateAsync(PedidoDto pedidoDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(pedidoDto);
            using var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<PedidoDto>>(jsonResponse);
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

        public async Task<Response<PedidoDto>> GetByNameAsync(string nombre)
        {
            var url = $"{_baseURL}{_endpoint}/nombre/{nombre}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<PedidoDto>>(jsonResponse);
        }
    }
}
