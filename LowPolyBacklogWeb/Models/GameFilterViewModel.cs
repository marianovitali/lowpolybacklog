namespace LowPolyBacklogWeb.Models
{
    public class GameFilterViewModel
    {
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public int? Year { get; set; }
        public int PageNumber { get; set; } = 1;
    }
}
