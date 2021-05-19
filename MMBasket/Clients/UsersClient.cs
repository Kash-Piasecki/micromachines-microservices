using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MMBasket.Data;

namespace MMBasket.Clients
{
    public class UsersClient
    {
        private readonly HttpClient _httpClient;

        public UsersClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserReadDto>> GetUsers()
        {
            var users = await _httpClient.GetFromJsonAsync<IEnumerable<UserReadDto>>("/Users");
            return users;
        }
        
        public async Task<UserReadDto> GetUser(Guid id)
        {
            var user = await _httpClient.GetFromJsonAsync<UserReadDto>($"/Users/{id}");
            return user;
        }
    }
}