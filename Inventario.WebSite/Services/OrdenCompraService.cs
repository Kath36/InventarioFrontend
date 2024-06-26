using Newtonsoft.Json;
using Inventario.Core.Http;
using Inventario.Api.Dto;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.WebSite.Services
{
    public class OrdenCompraService : IOrdenCompraService
    {
        private readonly string _baseURL = "http://localhost:5209/";
        private readonly string _endpoint = "api/OrdenCompra";

        public OrdenCompraService()
        {
        }

        public async Task<Response<List<OrdenCompraDto>>> GetAllAsync()
        {
            var url = $"{_baseURL}{_endpoint}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<List<OrdenCompraDto>>>(jsonResponse);
        }

        public async Task<Response<OrdenCompraDto>> GetByIdAsync(int id)
        {
            var url = $"{_baseURL}{_endpoint}/{id}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<OrdenCompraDto>>(jsonResponse);
        }
        public async Task<Response<OrdenCompraDto>> SaveAsync(OrdenCompraDto ordenCompraDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(ordenCompraDto);
            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                var errorObj = JsonConvert.DeserializeObject<Response<OrdenCompraDto>>(errorResponse);
                return errorObj;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<OrdenCompraDto>>(jsonResponse);
        }



        public async Task<Response<OrdenCompraDto>> UpdateAsync(OrdenCompraDto ordenCompraDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(ordenCompraDto);
            using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync(url, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<OrdenCompraDto>>(jsonResponse);
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var url = $"{_baseURL}{_endpoint}/{id}";
            using var client = new HttpClient();
            var response = await client.DeleteAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<bool>>(jsonResponse);
        }

        public async Task<Response<OrdenCompraDto>> GetByNameAsync(string nombre)
        {
            var url = $"{_baseURL}{_endpoint}/nombre/{nombre}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<OrdenCompraDto>>(jsonResponse);
        }
    }
}
