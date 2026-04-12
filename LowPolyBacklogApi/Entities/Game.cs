namespace LowPolyBacklogApi.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public required string Title { get; set; }
        public string? Synopsis { get; set; }
        public int ReleaseYear { get; set; }

        public string? Developer { get; set; }
        public string? CoverImageUrl { get; set; }

        public int DiscCount { get; set; } = 1;

        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    }
}
