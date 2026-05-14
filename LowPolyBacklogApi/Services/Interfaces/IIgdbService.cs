using LowPolyBacklogApi.DTOs.Igdb;

namespace LowPolyBacklogApi.Services.Interfaces
{
    public interface IIgdbService
    {
        Task<List<IgdbSearchResultDto>> SearchGamesAsync(string query);
    }
}
