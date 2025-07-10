using System.Text;
using System.Text.Json;

namespace TextAndWorkApp.Services;

public class OpenAIService(HttpClient httpClient, IConfiguration configuration)
{
    private readonly string _apiKey = configuration["OpenAI:ApiKey"] 
                                      ?? throw new ArgumentNullException("OpenAI ApiKey not found in configuration");
    private readonly string _apiUrl = "https://api.openai.com/v1/chat/completions";

    public async Task<string> GetCompletionAsync(string prompt)
    {
        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = prompt }
            },
            max_tokens = 1000
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json");

        httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);

        var response = await httpClient.PostAsync(_apiUrl, content);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var responseObject = JsonSerializer.Deserialize<JsonElement>(responseBody);
        
        return responseObject
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? string.Empty;
    }
}