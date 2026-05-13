using AutoMapper;
using LowPolyBacklogApi.DTOs.Dashboard;
using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.Entities;
using LowPolyBacklogApi.Repositories.Interfaces;
using LowPolyBacklogApi.Services.Interfaces;

namespace LowPolyBacklogApi.Services.Implementations
{
    public class DashboardService : IDashboardService
    {
        private readonly IBacklogRepository _backlogRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public DashboardService(IBacklogRepository backlogRepository, IGameRepository gameRepository, IMapper mapper)
        {
            _backlogRepository = backlogRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<DashboardSummaryDto> GetSummaryAsync()
        {
            var allBacklogs = await _backlogRepository.GetAllAsync();

            return new DashboardSummaryDto
            {
                TotalHoursPlayed = CalculateTotalHours(allBacklogs),
                CompletedGames = CountByStatus(allBacklogs, PlayStatus.Platinumed),
                PendingGames = CountByStatus(allBacklogs, PlayStatus.Pending),
                CurrentlyPlaying = GetActivePlays(allBacklogs),
                MonthlyRecommendations = await GetMonthlyRecommendations()
            };
        }

        private int CalculateTotalHours(IEnumerable<BacklogEntry> entries)
        => entries.Sum(b => b.HoursPlayed);

        private int CountByStatus(IEnumerable<BacklogEntry> entries, PlayStatus status)
            => entries.Count(b => b.Status == status);

        private List<ActivePlayDto> GetActivePlays(IEnumerable<BacklogEntry> entries)
        {
            return entries
                .Where(b => b.Status == PlayStatus.Playing)
                .Take(3)
                .Select(b => new ActivePlayDto
                {
                    GameId = b.GameId,
                    Title = b.Game.Title,
                    CoverImageUrl = b.Game.CoverImageUrl,
                    HoursPlayed = b.HoursPlayed
                }).ToList();
        }

        private async Task<List<GameResponseDto>> GetMonthlyRecommendations()
        {
            var queryParams = new GameQueryParameters { PageNumber = 1, PageSize = 5 };
            var (recommendedGames, _) = await _gameRepository.GetAllAsync(queryParams);
            return _mapper.Map<List<GameResponseDto>>(recommendedGames);
        }

    }
}
