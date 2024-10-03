using API.Data.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    public class SuppliersService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public SuppliersService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<Supplier>> GetSuppliersAsync()
        {
            var response = await _httpClient.GetAsync("suppliers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<Supplier>>();
            return content;
        }

        public async Task<Supplier> GetSupplierByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"suppliers/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<Supplier>();
            return content;
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            var json = JsonConvert.SerializeObject(supplier);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("suppliers", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            var json = JsonConvert.SerializeObject(supplier);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"suppliers/{supplier.suId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSupplierAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"suppliers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
