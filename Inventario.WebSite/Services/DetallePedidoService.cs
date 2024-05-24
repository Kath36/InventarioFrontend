using System.Text;
using Newtonsoft.Json;
using Inventario.Core.Http;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Services
{
    public class DetallePedidoService : IDetallePedidoService
    {
        private readonly string _baseURL = "http://localhost:5209/";
        private readonly string _endpoint = "api/DetallePedidos";

        public DetallePedidoService()
        {

        }

        public async Task<Response<List<DetallePedidoDto>>> GetAllAsync()
        {
            var url = $"{_baseURL}{_endpoint}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<List<DetallePedidoDto>>>(jsonResponse);
        }

        public async Task<Response<DetallePedidoDto>> GetById(int id)
        {
            var url = $"{_baseURL}{_endpoint}/{id}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<DetallePedidoDto>>(jsonResponse);
        }

        public async Task<Response<DetallePedidoDto>> SaveAsync(DetallePedidoDto detallePedidoDto)
        {var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(detallePedidoDto);
            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<Response<DetallePedidoDto>>(errorResponse);
                return errorObj;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<DetallePedidoDto>>(jsonResponse);
        }

        public async Task<Response<DetallePedidoDto>> UpdateAsync(DetallePedidoDto detallePedidoDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(detallePedidoDto);
            using var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<DetallePedidoDto>>(jsonResponse);
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

        public async Task<Response<DetallePedidoDto>> GetByMaterialIdAsync(int materialId)
        {
            var url = $"{_baseURL}{_endpoint}/material/{materialId}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<DetallePedidoDto>>(jsonResponse);
        }
    }
}
