using Newtonsoft.Json;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7
{
    public class BonusCardsService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public BonusCardsService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<BonusCard>> GetBonusCardsAsync()
        {
            var response = await _httpClient.GetAsync("bonuscards");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<BonusCard>>();
            return content;
        }

        public async Task<BonusCard> GetBonusCardByIdAsync(int bcId)
        {
            var response = await _httpClient.GetAsync($"bonuscards/{bcId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<BonusCard>();
            return content;
        }

        public async Task AddBonusCardAsync(float discount, Customer customer)
        {
            BonusCard bonusCard = new BonusCard();
            bonusCard.cuId = customer.cuId;
            bonusCard.discount = discount;
            var json = JsonConvert.SerializeObject(bonusCard);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("bonuscards", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateBonusCardAsync(BonusCard bonusCard)
        {
            var json = JsonConvert.SerializeObject(bonusCard);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"bonuscards/{bonusCard.bcId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBonusCardAsync(int bcId)
        {
            var response = await _httpClient.DeleteAsync($"bonuscards/{bcId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
