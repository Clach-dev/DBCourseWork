using Newtonsoft.Json;
using spp3.Data.Models;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace for_db7.Services
{
    public class CustomersService
    {
        public readonly HttpClient _httpClient;
        public const string BaseUrl = "https://localhost:7088/";

        public CustomersService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<ObservableCollection<Customer>> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync("customers");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<Customer>>();
            return content;
        }

        public async Task<Customer> GetCustomerIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"customers/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<Customer>();
            return content;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("customers", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"customers/{customer.cuId}", data);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"customers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}



//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.ComponentModel.DataAnnotations;
//using System.Net.Http;
//using System.Net.Http.Json;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using spp3.Data.Models;

//namespace for_db7
//{
//    public class CustomersService
//    {
//        public readonly HttpClient _httpClient;
//        public const string BaseUrl = "https://localhost:7088/";

//        public CustomersService()
//        {
//            _httpClient = new HttpClient
//            {
//                BaseAddress = new Uri(BaseUrl)
//            };
//        }

//        public async Task<ObservableCollection<Customer>> GetCustomersAsync()
//        {
//            var response = await _httpClient.GetAsync("customers");
//            response.EnsureSuccessStatusCode();
//            var content = await response.Content.ReadFromJsonAsync<ObservableCollection<Customer>>();
//            return content;
//        }

//        public async Task<Customer> GetCustomerByPhoneAsync(string phoneNumber)
//        {
//            var response = await _httpClient.GetAsync($"customers/{phoneNumber}");
//            response.EnsureSuccessStatusCode();
//            var content = await response.Content.ReadFromJsonAsync<Customer>();
//            return content;
//        }

//        public async Task AddCustomerAsync(string secondName, int age, string gender, string phoneNumber)
//        {
//            var customer = new Customer
//            {
//                secondName = secondName,
//                age = age,
//                gender = gender,
//                phoneNumber = phoneNumber,
//                //
//                firstName = "test first name",
//                adress = "test adress",
//                patrynomic = "test patrynomic"
//            };

//            var json = JsonConvert.SerializeObject(customer);
//            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

//            var response = await _httpClient.PostAsync("customers", data);
//            response.EnsureSuccessStatusCode();
//        }

//        public async Task UpdateCustomerAsync(string oldPhoneNumber, string secondName, int age, string gender, string phoneNumber)
//        {
//            var customer = new Customer
//            {
//                secondName = secondName,
//                age = age,
//                gender = gender,
//                phoneNumber = phoneNumber
//            };

//            var json = JsonConvert.SerializeObject(customer);
//            var data = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

//            var response = await _httpClient.PutAsync($"customers/{oldPhoneNumber}", data);
//            response.EnsureSuccessStatusCode();
//        }

//        public async Task DeleteCustomerAsync(string phoneNumber)
//        {
//            var response = await _httpClient.DeleteAsync($"customers/{phoneNumber}");
//            response.EnsureSuccessStatusCode();
//        }
//    }
//}
