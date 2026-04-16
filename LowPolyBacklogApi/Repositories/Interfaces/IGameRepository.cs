using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.Entities;

namespace LowPolyBacklogApi.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<(IEnumerable<Game> items, int totalCount)> GetAllAsync(GameQueryParameters parameters);
        Task<Game?> GetByIdAsync(int id);
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
        Task DeleteAsync(Game game);

    }
}

