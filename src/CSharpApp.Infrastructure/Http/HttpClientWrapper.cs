using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace CSharpApp.Infrastructure.Http;

public class HttpClientWrapper : IHttpClientWrapper
{
    private readonly HttpClient _client;
    private readonly ILogger<HttpClientWrapper> _logger;

    public HttpClientWrapper(HttpClient client, ILogger<HttpClientWrapper> logger, IConfiguration configuration)
    {
        _client = client;
        _logger = logger;
        _client.BaseAddress = new Uri(configuration["BaseUrl"]);
    }

    public async Task<T?> GetAsync<T>(string uri)
    {
        try
        {
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                _logger.LogWarning("GET request to {Uri} failed with status code {StatusCode}.", uri, response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while sending a GET request to {Uri}.", uri);
        }
        return default;
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(string uri, TRequest content)
    {
        try
        {
            var response = await _client.PostAsJsonAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            else
            {
                _logger.LogWarning("POST request to {Uri} failed with status code {StatusCode}.", uri, response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while sending a POST request to {Uri}.", uri);
        }
        return default;
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(string uri, TRequest content)
    {
        try
        {
            var response = await _client.PutAsJsonAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            else
            {
                _logger.LogWarning("PUT request to {Uri} failed with status code {StatusCode}.", uri, response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while sending a PUT request to {Uri}.", uri);
        }
        return default;
    }

    public async Task<bool> DeleteAsync(string uri)
    {
        try
        {
            var response = await _client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _logger.LogWarning("DELETE request to {Uri} failed with status code {StatusCode}.", uri, response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while sending a DELETE request to {Uri}.", uri);
        }
        return false;
    }
}