using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ChatBot.Models;

namespace ChatBot.Services
{
    public class DeepSeekService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl;

        public DeepSeekService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["DeepSeek:ApiKey"];
            _apiUrl = configuration["DeepSeek:ApiUrl"];
        }

        public async Task<string> GetDeepSeekResponse(List<Message> messages)
        {
            var requestBody = new
            {
                model = "mistralai/mistral-7b-instruct",
                messages = messages.Select(m => new
                {
                    content = m.Content
                }).ToList()
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync(_apiUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error calling DeepSeek API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);
            return jsonResponse.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }
    }
}
