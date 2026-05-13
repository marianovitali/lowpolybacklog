using LowPolyBacklogApi.DTOs.Game;

namespace LowPolyBacklogApi.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        // USER METRICS
        public int TotalHoursPlayed { get; set; }
        public int CompletedGames { get; set; }
        public int PendingGames { get; set; }

        // PLAYING NOW
        public List<ActivePlayDto> CurrentlyPlaying { get; set; } = new List<ActivePlayDto>();

        // RECOMMENDATIONS OF THE MONTH
        public List<GameResponseDto> MonthlyRecommendations { get; set; } = new List<GameResponseDto>();
    }
}
