using System.Text;
using System.Text.Json;
using EasyEmployerBackend.Models;

namespace EasyEmployerBackend.Services;

public interface ILoketService
{
    Task<CreateEmployerResponse> CreateEmployerAsync(string accessToken, string werkgevernaam);
}

public class LoketService : ILoketService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LoketService> _logger;
    private const string LoketApiUrl = "https://api.loket-ontw.nl/v2/providers/3b34f479-7b46-4944-b579-acee7257e0cc/employers";

    public LoketService(HttpClient httpClient, ILogger<LoketService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<CreateEmployerResponse> CreateEmployerAsync(string accessToken, string werkgevernaam)
    {
        try
        {
            var requestBody = new
            {
                name = werkgevernaam
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            _logger.LogInformation("Creating employer with name: {Werkgevernaam}", werkgevernaam);

            var response = await _httpClient.PostAsync(LoketApiUrl, content);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to create employer. Status: {StatusCode}, Error: {Error}", 
                    response.StatusCode, errorContent);
                throw new HttpRequestException($"Failed to create employer: {response.StatusCode} - {errorContent}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Employer created successfully. Response: {Response}", responseContent);

            var result = JsonSerializer.Deserialize<CreateEmployerResponse>(responseContent, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (result == null)
            {
                throw new InvalidOperationException("Failed to deserialize response from Loket API");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employer");
            throw;
        }
    }
}
