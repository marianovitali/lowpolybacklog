using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.Entities;

namespace LowPolyBacklogApi.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameResponseDto>> GetAllGamesAsync(string? title, string? genre, int? year);
        Task<GameResponseDto?> GetGameByIdAsync(int id);
        Task<GameResponseDto> CreateGameAsync(GameCreateDto game);
        Task<GameResponseDto> UpdateAsync(GameUpdateDto game, int id);
        Task DeleteAsync(int id);
    }
}
