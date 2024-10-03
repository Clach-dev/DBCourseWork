using API.Data.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    public class TradeOutletsService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public TradeOutletsService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<TradeOutlet>> GetTradeOutletsAsync()
        {
            var response = await _httpClient.GetAsync("tradeoutlets");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<TradeOutlet>>();
            return content;
        }

        public async Task<TradeOutlet> GetTradeOutletIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"tradeoutlets/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<TradeOutlet>();
            return content;
        }

        public async Task AddTradeOutletAsync(TradeOutlet tradeOutlet)
        {
            var json = JsonConvert.SerializeObject(tradeOutlet);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("tradeoutlets", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateTradeOutletAsync(TradeOutlet tradeOutlet)
        {
            var json = JsonConvert.SerializeObject(tradeOutlet);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"tradeoutlets/{tradeOutlet.toId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTradeOutletAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"tradeoutlets/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
