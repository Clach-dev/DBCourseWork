using API.Data.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    public class SellersService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public SellersService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<Seller>> GetSellersAsync()
        {
            var response = await _httpClient.GetAsync("sellers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<Seller>>();
            return content;
        }

        public async Task<Seller> GetSellerIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"sellers/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<Seller>();
            return content;
        }

        public async Task AddSellerAsync(Seller seller)
        {
            var json = JsonConvert.SerializeObject(seller);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("sellers", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateSellerAsync(Seller seller)
        {
            Seller sellerToSend = new Seller()
            {
                firstName = seller.firstName,
                secondName = seller.secondName,
                patrynomic = seller.patrynomic,
                endOfContract = seller.endOfContract,
                phoneNumber = seller.phoneNumber,
                salary = seller.salary,
                osId = seller.osId
            };
            var json = JsonConvert.SerializeObject(sellerToSend);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"sellers/{seller.selId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSellerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"sellers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}