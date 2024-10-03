using API.Data.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7
{
    public class DealsService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public DealsService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<Deal>> GetDealsAsync()
        {
            var response = await _httpClient.GetAsync("deals");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<Deal>>();
            return content;
        }

        public async Task<Deal> GetDealByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"deals/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<Deal>();
            return content;
        }

        public async Task AddDealAsync(Deal deal)
        {
            var json = JsonConvert.SerializeObject(deal);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("deals", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateDealAsync(Deal deal)
        {
            var json = JsonConvert.SerializeObject(deal);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"deals/{deal.dlId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSellerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"deals/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
