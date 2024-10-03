using Newtonsoft.Json;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    public class ProductTypesService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public ProductTypesService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<ProductType>> GetProductTypesAsync()
        {
            var response = await _httpClient.GetAsync("producttypes");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<ProductType>>();
            return content;
        }

        public async Task<ProductType> GetProductTypeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"producttypes/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ProductType>();
            return content;
        }

        public async Task AddProductTypeAsync(ProductType productType)
        {
            var json = JsonConvert.SerializeObject(productType);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("producttypes", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductTypeAsync(ProductType productType)
        {
            var json = JsonConvert.SerializeObject(productType);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"producttypes/{productType.ptId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductTypeAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"producttypes/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
