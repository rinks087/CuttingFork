
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Net.Http.Json;
using ITGBrands.CSPF.Models;

namespace ITGBrands.CSPF.Services
{
    public interface IDataService
    {
          

        Task<ApiResponse> FetchDataFromApiAsync();

     
    }
    public class DataService : IDataService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://localhost:5001/api/get-saratogaInfo"; // Replace with your actual URL
        private const string ClientId = "client"; // Replace with your client ID
        private const string ClientSecret = "secret"; // Replace with your client secret

        public DataService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetAccessTokenAsync()
        {
            var tokenUrl = "https://localhost:5001/connect/token";

            var client = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[]
            {
        new KeyValuePair<string, string>("client_id", "client"),
        new KeyValuePair<string, string>("client_secret", "secret"),
        new KeyValuePair<string, string>("grant_type", "client_credentials"),
    });

            var response = await client.PostAsync(tokenUrl, formContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);

            return tokenResponse.AccessToken;
        }

        public class TokenResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }
        }

     


        public async Task<ApiResponse> FetchDataFromApiAsync()
        {
            if (DataCache.IsCacheValid() && DataCache.CachedData != null)
            {
                return DataCache.CachedData;
            }


            var token = await GetAccessTokenAsync();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

           
            var response = await _httpClient.GetAsync(ApiUrl);
            response.EnsureSuccessStatusCode();


            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("API call failed");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null)
            {
                throw new Exception("Deserialization failed");
            }

            // Update cache
            DataCache.CachedData = result;
            DataCache.CacheExpiration = DateTime.Now.AddMinutes(10); // Cache for 10 minutes

            return result;
        }





    }
}
