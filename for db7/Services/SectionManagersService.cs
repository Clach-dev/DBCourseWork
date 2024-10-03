using API.Data.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    class SectionManagersService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public SectionManagersService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<SectionManager>> GetSectionManagersAsync()
        {
            var response = await _httpClient.GetAsync("sectionmanagers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<SectionManager>>();
            return content;
        }

        public async Task<SectionManager> GetSectionManagerIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"sectionmanagers/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<SectionManager>();
            return content;
        }

        public async Task AddSectionManagerAsync(SectionManager sectionManager)
        {
            var json = JsonConvert.SerializeObject(sectionManager);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("sectionmanagers", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateSectionManagerAsync(SectionManager sectionManager)
        {
            SectionManager sectionManagerToSend = new SectionManager()
            {
                firstName = sectionManager.firstName,
                secondName = sectionManager.secondName,
                patrynomic = sectionManager.patrynomic,
                salary = sectionManager.salary,
                phoneNumber = sectionManager.phoneNumber,
                experience = sectionManager.experience,
                osId = sectionManager.osId
            };
            var json = JsonConvert.SerializeObject(sectionManagerToSend);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"sectionmanagers/{sectionManager.smId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteSectionManagerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"sectionmanagers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
