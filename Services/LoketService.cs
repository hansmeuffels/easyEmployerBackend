using System.Text;
using System.Text.Json;
using EasyEmployerBackend.Models;
using Microsoft.Extensions.Options;

namespace EasyEmployerBackend.Services;

public interface ILoketService
{
    Task<CreateEmployerResponse> CreateEmployerAsync(string accessToken, string werkgevernaam);
}

public class LoketService : ILoketService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<LoketService> _logger;
    private readonly LoketApiSettings _settings;

    public LoketService(HttpClient httpClient, ILogger<LoketService> logger, IOptions<LoketApiSettings> settings)
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = settings.Value;
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

            var employersUrl = $"{_settings.BaseUrl}/providers/{_settings.ProviderId}/employers";

            var request = new HttpRequestMessage(HttpMethod.Post, employersUrl)
            {
                Content = content
            };
            request.Headers.Add("Authorization", $"Bearer {accessToken}");

            _logger.LogInformation("Creating employer with name: {Werkgevernaam}", werkgevernaam);

            var response = await _httpClient.SendAsync(request);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError("Failed to create employer. Status: {StatusCode}, Error: {Error}", 
                    response.StatusCode, errorContent);
                throw new HttpRequestException($"Failed to create employer. Status: {response.StatusCode}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Employer created successfully");

            var result = JsonSerializer.Deserialize<CreateEmployerResponse>(responseContent, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (result == null)
            {
                throw new InvalidOperationException("Failed to deserialize response from Loket API");
            }

            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP error while creating employer");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating employer");
            throw;
        }
    }
}
