using LowPolyBacklogApi.Services.Interfaces;
using LowPolyBacklogApi.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LowPolyBacklogApi.Services.Implementations
{
    public class IgdbAuthService : IIgdbAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IgdbSettings _settings;
        private readonly IMemoryCache _cache;

        private const string CacheKey = "IgdbAccessToken";

        public IgdbAuthService(HttpClient httpClient, IOptions<IgdbSettings> settings, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _cache = cache;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            if (_cache.TryGetValue(CacheKey, out string? cachedToken) && !string.IsNullOrEmpty(cachedToken))
            {
                return cachedToken;
            }

            var requestData = new Dictionary<string, string>
            {
                { "client_id", _settings.ClientId },
                { "client_secret", _settings.ClientSecret },
                { "grant_type", "client_credentials" }
            };

            var requestContent = new FormUrlEncodedContent(requestData);

            var response = await _httpClient.PostAsync("https://id.twitch.tv/oauth2/token", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al autenticarse con Twitch/IGDB. Revisá tus credenciales.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var authData = JsonSerializer.Deserialize<TwitchAuthResponse>(jsonResponse);

            if (authData == null || string.IsNullOrEmpty(authData.AccessToken))
            {
                throw new Exception("Twitch devolvió un token vacío o inválido.");
            }

            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(authData.ExpiresIn - 3600));

            _cache.Set(CacheKey, authData.AccessToken, cacheOptions);

            return authData.AccessToken;
        }

        private class TwitchAuthResponse
        {
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; } = string.Empty;

            [JsonPropertyName("expires_in")]
            public int ExpiresIn { get; set; }
        }
    }
}
