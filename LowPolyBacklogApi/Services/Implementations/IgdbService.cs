using LowPolyBacklogApi.DTOs.Igdb;
using LowPolyBacklogApi.Services.Interfaces;
using LowPolyBacklogApi.Settings;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace LowPolyBacklogApi.Services.Implementations
{
    public class IgdbService : IIgdbService
    {
        private readonly HttpClient _httpClient;
        private readonly IIgdbAuthService _authService;
        private readonly IgdbSettings _settings;

        public IgdbService(HttpClient httpClient, IIgdbAuthService authService, IOptions<IgdbSettings> settings)
        {
            _httpClient = httpClient;
            _authService = authService;
            _settings = settings.Value;
        }

        public async Task<List<IgdbSearchResultDto>> SearchGamesAsync(string query)
        {
            var token = await _authService.GetAccessTokenAsync();

            var request = BuildIgdbRequest(query, token);

            var rawGames = await ExecuteRequestAsync(request);

            return MapToCleanDtos(rawGames);
        }

        private HttpRequestMessage BuildIgdbRequest(string query, string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.igdb.com/v4/games");
            request.Headers.Add("Client-ID", _settings.ClientId);
            request.Headers.Add("Authorization", $"Bearer {token}");

            string apicalypseQuery = $@"
                search ""{query}""; 
                fields name, summary, first_release_date, cover.image_id, genres.name, 
                involved_companies.developer, involved_companies.company.name;
                where platforms = (7);
                limit 10;
            ";

            request.Content = new StringContent(apicalypseQuery);
            return request;
        }

        private async Task<List<IgdbGameResponseDto>> ExecuteRequestAsync(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorDetail = await response.Content.ReadAsStringAsync();
                throw new Exception($"IGDB Error: {errorDetail}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var rawGames = JsonSerializer.Deserialize<List<IgdbGameResponseDto>>(jsonResponse);

            return rawGames ?? new List<IgdbGameResponseDto>();
        }

        private List<IgdbSearchResultDto> MapToCleanDtos(List<IgdbGameResponseDto> rawGames)
        {
            return rawGames.Select(g => new IgdbSearchResultDto
            {
                IgdbId = g.Id,
                Title = g.Name,
                Synopsis = g.Summary,
                ReleaseYear = g.FirstReleaseDate.HasValue
                    ? DateTimeOffset.FromUnixTimeSeconds(g.FirstReleaseDate.Value).Year
                    : null,
                CoverImageUrl = g.Cover != null
                    ? $"https://images.igdb.com/igdb/image/upload/t_cover_big/{g.Cover.ImageId}.jpg"
                    : null,
                Genres = g.Genres != null
                    ? g.Genres.Select(x => x.Name).ToList()
                    : new List<string>(),
                Developer = g.InvolvedCompanies != null
                    ? g.InvolvedCompanies.FirstOrDefault(c => c.IsDeveloper)?.Company?.Name
                    : null,
                DiscCount = 1
            }).ToList();
        }
    }
}
