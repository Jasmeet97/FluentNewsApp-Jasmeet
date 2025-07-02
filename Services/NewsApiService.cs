using FluentNewsApp_Jasmeet.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace FluentNewsApp_Jasmeet.Services
{
    public class NewsApiService
    {
        private readonly HttpClient _httpClient = new();
        private static IConfigurationRoot? Configuration { get; set; }
        private const string CustomUserAgent = "FluentNewsApp-Jasmeet/1.0 (Windows 11.0; WPF; .NET 9.0)";

        public NewsApiService()
        {
            // Accessing NewsApi API key and base url from appsettings.json
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        private static string FormUrl(string category)
        {
            //method to create a URL for the News API
            if (Configuration == null)
            {
                throw new Exception("App Configuration Missing.");
            }

            string? newsApiKey = Configuration["NewsApi:ApiKey"];
            string? baseUrl = Configuration["NewsApi:BaseUrl"];

            if (newsApiKey == null || baseUrl == null)
            {
                throw new Exception("API Credentials Missing.");
            }

            return $"{baseUrl}?country=us&category={category}&apiKey={newsApiKey}";
        }

        public async Task<List<Article>> GetNewsAsync(string category)
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", CustomUserAgent);
            HttpResponseMessage response = await _httpClient.GetAsync(FormUrl(category.ToLower()));

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Request failed with status {response.StatusCode}.");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var articlesData = JsonSerializer.Deserialize<NewsApiResponse>(jsonResponse) ?? throw new Exception("Failed to display articles.");

            if (articlesData.Status != "ok")
            {
                throw new Exception(articlesData.Message ?? "Something went wrong. Try again later.");
            }

            return articlesData.Articles ?? [];
        }
    }
}
