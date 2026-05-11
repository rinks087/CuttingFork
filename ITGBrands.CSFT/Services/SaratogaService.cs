using ITGBrands.CSFT.Models;
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


namespace ITGBrands.CSFT.Services
{
    public interface ISaratogaService
    {
       
        Task<string> SendDumpMessage(Msg msg, string endpoint, string returnCode, string returnDescription);

        Task<ApiResponse> FetchDataFromApiAsync();

        Task<int> GetCount(DateTime recordDate);
    }
    public class SaratogaService : ISaratogaService
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "http://localhost:5000/api/get-saratogaInfo"; // Replace with your actual URL
        private const string DumperApiUrl = "http://localhost:5000/api/get-DumperLocationDesc";
        private const string SaveDumpMessage = "http://localhost:5000/api/SendDumpMessage";
        private const string GetDumpCount = "http://localhost:5000/api/DumpCount?recordDate={recordDate:yyyy-MM-dd}";
        private const string ClientId = "client"; // Replace with your client ID
        private const string ClientSecret = "secret"; // Replace with your client secret

        public SaratogaService()
        {
           
            _httpClient = new HttpClient();
        }
    //    public async Task<string> GetAccessTokenAsync()
    //    {
    //        var tokenUrl = "https://10.0.2.2:5001/connect/token";

    //        var client = new HttpClient();
    //        var formContent = new FormUrlEncodedContent(new[]
    //        {
    //    new KeyValuePair<string, string>("client_id", "client"),
    //    new KeyValuePair<string, string>("client_secret", "secret"),
    //    new KeyValuePair<string, string>("grant_type", "client_credentials"),
    //});

    //        var response = await _httpClient.PostAsync(tokenUrl, formContent);
    //        response.EnsureSuccessStatusCode();

    //        var content = await response.Content.ReadAsStringAsync();
    //        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content);

    //        return tokenResponse.AccessToken;
    //    }

    //    public class TokenResponse
    //    {
    //        [JsonPropertyName("access_token")]
    //        public string AccessToken { get; set; }
    //    }




        public async Task<ApiResponse> FetchDataFromApiAsync()
        {
            // Check if cached data is valid
            if (DataCache.IsCacheValid() && DataCache.CachedData != null)
            {
                return DataCache.CachedData;
            }

            try
            {
                // Uncomment and implement token retrieval if needed
                // var token = await GetAccessTokenAsync();
                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Make the API call
                var response = await _httpClient.GetAsync(ApiUrl);
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response content
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ApiResponse>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? throw new Exception("Deserialization failed");

                // Update cache
                DataCache.CachedData = result;
                DataCache.CacheExpiration = DateTime.Now.AddMinutes(10); // Cache for 10 minutes

                return result;
            }
            catch (HttpRequestException httpRequestEx)
            {
                // Handle specific HTTP request exceptions
                Console.WriteLine($"Request error: {httpRequestEx.Message}");
                throw; // Rethrow to allow calling code to handle it
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON deserialization errors
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
                throw; // Rethrow to allow calling code to handle it
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow to allow calling code to handle it
            }
        }





        public async Task<string> SendDumpMessage(Msg msg, string endpoint, string returnCode, string returnDescription)
        {
            try
            {
                var xml = SerializeMsgToXml(msg);
                Console.WriteLine("Serialized XML:\n" + xml);

                // Prepare the HTTP content
                var content = new StringContent(xml, Encoding.UTF8, "application/xml");

                var fullUrl = $"https://localhost:5001/api/SendDumpMessage?endpoint={Uri.EscapeDataString(endpoint)}&returnCode={Uri.EscapeDataString(returnCode)}&returnDescription={Uri.EscapeDataString(returnDescription)}";


                // Retrieve the access token
               // var token = await GetAccessTokenAsync();
                //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Use the provided endpoint instead of a hardcoded one
                var response = await _httpClient.PostAsync(fullUrl, content);

                // Check if the response indicates success
                if (!response.IsSuccessStatusCode)
                {
                    // Log the response status code and content
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode}");
                    Console.WriteLine($"Response Content: {responseContent}");

                    // Optionally throw an exception or handle it as needed
                    throw new HttpRequestException($"Request failed with status code {response.StatusCode}");
                }

                // Log the return code and description if needed
                Console.WriteLine($"Return Code: {returnCode}");
                Console.WriteLine($"Return Description: {returnDescription}");

                // Read and return the response JSON as a string
                var responseJson = await response.Content.ReadAsStringAsync();
                return responseJson;
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");

                // Optionally rethrow or handle the exception
                throw;
            }
        }


        public async Task<int> GetCount(DateTime recordDate)
        {
            // Construct the API URL with the formatted date
            string apiUrl = $"https://localhost:5001/api/DumpCount?recordDate={recordDate:yyyy-MM-dd}";

            try
            {
                // Uncomment and implement token retrieval if needed
                // var token = await GetAccessTokenAsync();
                // _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Make the API call
                var response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throws if the response is not successful

                // Deserialize and return the record count
                var jsonResult = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<DumperCountResponse>(jsonResult, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Handle case insensitivity
                });

                return result?.recordCount ?? throw new Exception("Deserialization returned null.");
            }
            catch (HttpRequestException ex)
            {
                // Log and rethrow specific HTTP request exceptions
                Console.WriteLine($"Request error: {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                // Log and rethrow JSON deserialization errors
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log and rethrow any other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }


        private string SerializeMsgToXml(Msg msg)
        {
            var xmlSerializer = new XmlSerializer(typeof(Msg));

            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true // Omit the XML declaration
            };

            using (var stringWriter = new StringWriter())
            using (var xmlWriter = XmlWriter.Create(stringWriter, settings))
            {
                xmlSerializer.Serialize(xmlWriter, msg);
                return stringWriter.ToString();
            }
        }


    }
}
