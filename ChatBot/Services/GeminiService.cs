﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ChatBot.Models;

namespace ChatBot.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://generativelanguage.googleapis.com/v1/models/gemini-pro:generateContent";

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Gemini:ApiKey"];
        }

        public async Task<string> GetGeminiResponse(List<Message> messages)
        {
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = messages.Select(m => new { text = m.Content }).ToArray()
                    }
                }
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_apiUrl}?key={_apiKey}", jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error calling Gemini API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");

            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseBody);

            return jsonResponse.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();
        }
    }
}
