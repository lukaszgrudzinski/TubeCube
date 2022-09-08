using MonkeyCache;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TubeCube.Framework.Exceptions;
using TubeCube.Framework.Extensions;

namespace TubeCube.Framework.Services;

internal class RestServiceBase
{
    private readonly IConnectivity _connectivity;
    private readonly IBarrel _cacheBarrel;
    private HttpClient? _httpClient;

    protected RestServiceBase(IConnectivity connectivity, IBarrel cahceBarrel)
    {
        _connectivity = connectivity;
        _cacheBarrel = cahceBarrel;
    }

    protected void SetBaseUrl(string baseUrl)
    {
        _httpClient = new()
        {
            BaseAddress = new Uri(baseUrl)
        };

        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    protected void AddHttpHeader(string name, string value) =>
        _httpClient?.DefaultRequestHeaders?.Add(name, value);

    protected async Task<T?> GetAsync<T>(string resource, int cacheDuration = 24)
    {
        string json = await GetJsonAsync<T>(resource, cacheDuration);

        return JsonSerializer.Deserialize<T>(json);
    }

    private async Task<string> GetJsonAsync<T>(string resource, int cacheDuration)
    {
        string cleanCacheKey = resource.CleanCacheKey();

        if (_cacheBarrel is not null)
        {
            string? cachedData = _cacheBarrel.Get<string>(cleanCacheKey);

            if (cachedData != null && !_cacheBarrel.IsExpired(cleanCacheKey))
            {
                return cachedData;
            }

            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                return cachedData ?? throw new InternetConnectionException();
            }
        }

        //No Cache Found, or Cached data was not required, or Internet connection is also available
        if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            throw new InternetConnectionException();

        //Extract response from URI
        var response = await _httpClient.GetAsync(new Uri(_httpClient.BaseAddress, resource));

        //Check for valid response
        response.EnsureSuccessStatusCode();

        //Read Response
        string json = await response.Content.ReadAsStringAsync();

        //Save to Cache if required
        if (cacheDuration > 0 && _cacheBarrel is not null)
        {
            try
            {
                _cacheBarrel.Add(cleanCacheKey, json, TimeSpan.FromHours(cacheDuration));
            }
            catch { }
        }

        //Return the result
        return json;
    }


    protected async Task<HttpResponseMessage> PostAsync<T>(string uri, T payload)
    {
        var dataToPost = JsonSerializer.Serialize(payload);
        var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(new Uri(_httpClient.BaseAddress, uri), content);

        response.EnsureSuccessStatusCode();

        return response;
    }

    protected async Task<HttpResponseMessage> PutAsync<T>(string uri, T payload)
    {
        var dataToPost = JsonSerializer.Serialize(payload);
        var content = new StringContent(dataToPost, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(new Uri(_httpClient.BaseAddress, uri), content);

        response.EnsureSuccessStatusCode();

        return response;
    }

    protected async Task<HttpResponseMessage> DeleteAsync(string uri)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync(new Uri(_httpClient.BaseAddress, uri));

        response.EnsureSuccessStatusCode();

        return response;
    }
}
