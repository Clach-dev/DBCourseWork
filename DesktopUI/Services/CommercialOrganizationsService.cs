using API.Data.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    public class CommercialOrganizationsService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public CommercialOrganizationsService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<CommercialOrganization>> GetCommercialOrganizationsAsync()
        {
            var response = await _httpClient.GetAsync("commercialorganizations");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<CommercialOrganization>>();
            return content;
        }

        public async Task<CommercialOrganization> GetCommercialOrganizationIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"commercialorganizations/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<CommercialOrganization>();
            return content;
        }

        public async Task AddCommercialOrganizationAsync(CommercialOrganization commercialOrganization)
        {
            var json = JsonConvert.SerializeObject(commercialOrganization);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("commercialorganizations", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCommercialOrganizationAsync(CommercialOrganization commercialOrganization)
        {
            var json = JsonConvert.SerializeObject(commercialOrganization);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"commercialorganizations/{commercialOrganization.coId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCommercialOrganizationAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"commercialorganizations/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
