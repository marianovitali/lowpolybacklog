using LowPolyBacklogWeb.Models;

namespace LowPolyBacklogWeb.Services
{
    public interface IGameApiService
    {
        Task<PagedResponse<GameViewModel>> GetGamesAsync(GameFilterViewModel filters);
    }
}
