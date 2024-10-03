using API.Data.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7
{
    public class UsersService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public UsersService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<User>> GetUsersAsync()
        {
            var response = await _httpClient.GetAsync("users");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<User>>();
            return content;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"users/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<User>();
            return content;
        }

        public async Task AddUserAsync(User user)
        {
            user.urId = 2;

            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("users", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateUserAsync(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"users/{user.usId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"users/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
