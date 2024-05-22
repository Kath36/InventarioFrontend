using System.Text;
using Newtonsoft.Json;
using Inventario.Core.Http;
using Inventario.Api.Dto;

namespace Inventario.WebSite.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly string _baseURL = "http://localhost:5209/";
        private readonly string _endpoint = "api/Auth";

        public UsuarioService()
        {

        }

        public async Task<Response<List<UsuarioDto>>> GetAllAsync()
        {
            var url = $"{_baseURL}{_endpoint}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<List<UsuarioDto>>>(jsonResponse);
        }

        public async Task<Response<UsuarioDto>> GetById(int id)
        {
            var url = $"{_baseURL}{_endpoint}/{id}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<UsuarioDto>>(jsonResponse);
        }

        public async Task<Response<UsuarioDto>> SaveAsync(UsuarioDto usuarioDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(usuarioDto);
            using var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<UsuarioDto>>(jsonResponse);
        }

        public async Task<Response<UsuarioDto>> UpdateAsync(UsuarioDto usuarioDto)
        {
            var url = $"{_baseURL}{_endpoint}";
            var jsonRequest = JsonConvert.SerializeObject(usuarioDto);
            using var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
            using var client = new HttpClient();
            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<UsuarioDto>>(jsonResponse);
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

        public async Task<Response<UsuarioDto>> GetByNameAsync(string name)
        {
            var url = $"{_baseURL}{_endpoint}/nombre/{name}";
            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Response<UsuarioDto>>(jsonResponse);
        }
        
            public async Task<Response<UsuarioDto>> AuthenticateAsync(string email, string password)
            {
                var url = $"{_baseURL}{_endpoint}/authenticate";
                var loginRequest = new LoginRequestDto { Email = email, Contraseña = password };
                var jsonRequest = JsonConvert.SerializeObject(loginRequest);
                using var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                using var client = new HttpClient();

                try
                {
                    var response = await client.PostAsync(url, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var errorResponse = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error response: {errorResponse}");
                        return new Response<UsuarioDto>
                        {
                            Success = false,
                            Message = "Correo electrónico o contraseña incorrectos."
                        };
                    }

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(jsonResponse))
                    {
                        throw new Exception("Respuesta vacía del servidor de autenticación.");
                    }

                    return JsonConvert.DeserializeObject<Response<UsuarioDto>>(jsonResponse);
                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine($"Error en la solicitud HTTP: {httpEx.Message}");
                    return new Response<UsuarioDto>
                    {
                        Success = false,
                        Message = "Error de conexión al servidor de autenticación."
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en el método AuthenticateAsync: {ex}");
                    return new Response<UsuarioDto>
                    {
                        Success = false,
                        Message = "Ocurrió un error al procesar la solicitud."
                    };
                }
            }

        }
    }
