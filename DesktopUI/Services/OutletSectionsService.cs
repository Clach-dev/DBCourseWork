using API.Data.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    class OutletSectionsService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public OutletSectionsService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<OutletSection>> GetOutletSectionsAsync()
        {
            var response = await _httpClient.GetAsync("outletsections");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<OutletSection>>();
            return content;
        }

        public async Task<OutletSection> GetOutletSectionIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"outletsections/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<OutletSection>();
            return content;
        }

        public async Task AddOutletSectionAsync(OutletSection outletSection)
        {
            var json = JsonConvert.SerializeObject(outletSection);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("outletsections", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateOutletSectionAsync(OutletSection outletSection)
        {
            var json = JsonConvert.SerializeObject(outletSection);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"outletsections/{outletSection.osId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteOutletSectionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"outletsections/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
