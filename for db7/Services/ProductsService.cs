using Newtonsoft.Json;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    public class ProductsService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public ProductsService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("products");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<Product>>();
            return content;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"products/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<Product>();
            return content;
        }

        public async Task AddProductAsync(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("products", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"products/{product.prId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
