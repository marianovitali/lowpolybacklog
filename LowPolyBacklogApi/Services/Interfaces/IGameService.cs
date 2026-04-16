using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.Entities;

namespace LowPolyBacklogApi.Services.Interfaces
{
    public interface IGameService
    {
        Task<(IEnumerable<GameResponseDto> items, int totalCount)> GetAllGamesAsync(GameQueryParameters parameters);
        Task<GameDetailsResponseDto?> GetGameByIdAsync(int id);
        Task<GameResponseDto> CreateGameAsync(GameCreateDto game);
        Task<GameResponseDto> UpdateAsync(GameUpdateDto game, int id);
        Task DeleteAsync(int id);
    }
}
