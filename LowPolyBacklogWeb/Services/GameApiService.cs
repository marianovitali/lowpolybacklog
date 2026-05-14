using LowPolyBacklogWeb.Models;
using System.Text.Json;

namespace LowPolyBacklogWeb.Services
{
    public class GameApiService : IGameApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GameApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<PagedResponse<GameViewModel>> GetGamesAsync(GameFilterViewModel filters)
        {
            var client = _httpClientFactory.CreateClient("LowPolyBacklogApi");

            var queryParams = new List<string> { $"pageNumber={filters.PageNumber}" };


            if (!string.IsNullOrEmpty(filters.Title)) queryParams.Add($"title={Uri.EscapeDataString(filters.Title)}");
            if (!string.IsNullOrEmpty(filters.Genre)) queryParams.Add($"genre={Uri.EscapeDataString(filters.Genre)}");
            if (filters.Year.HasValue) queryParams.Add($"year={filters.Year.Value}");

            var queryString = string.Join("&", queryParams);
            var response = await client.GetAsync($"api/games?{queryString}"); 
            
            if (!response.IsSuccessStatusCode)
            {
                return new PagedResponse<GameViewModel>();
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PagedResponse<GameViewModel>>(
                jsonString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            return result ?? new PagedResponse<GameViewModel>();
        }

    }
}
