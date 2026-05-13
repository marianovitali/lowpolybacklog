namespace LowPolyBacklogApi.DTOs.Dashboard
{
    public class ActivePlayDto
    {
        public int GameId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? CoverImageUrl { get; set; }
        public int HoursPlayed { get; set; } 
    }
}
