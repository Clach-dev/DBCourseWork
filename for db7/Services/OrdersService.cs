using API.Data.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    public class OrdersService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public OrdersService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<Order>> GetOrdersAsync()
        {
            var response = await _httpClient.GetAsync("orders");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<Order>>();
            return content;
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"orders/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<Order>();
            return content;
        }

        public async Task AddOrderAsync(Order order)
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("orders", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"orders/{order.orId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"orders/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
