namespace LowPolyBacklogApi.DTOs.Igdb
{
    public class IgdbSearchResultDto
    {
        public int IgdbId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public int? ReleaseYear { get; set; } 
        public string? CoverImageUrl { get; set; } 
        public List<string> Genres { get; set; } = new List<string>();
        public string? Developer { get; set; }
        public int DiscCount { get; set; } = 1; 
    }
}
