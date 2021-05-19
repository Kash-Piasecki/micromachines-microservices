using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MMBasket.Data;

namespace MMBasket.Clients
{
    public class ProductsClient
    {
        private readonly HttpClient _httpClient;

        public ProductsClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsList()
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductReadDto>>("/products");
            return products;
        }
    }
}