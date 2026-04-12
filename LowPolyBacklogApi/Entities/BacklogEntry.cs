using System.ComponentModel.DataAnnotations;

namespace LowPolyBacklogApi.Entities
{
    public class BacklogEntry
    {
        public int Id { get; set; }

        public PlayStatus Status { get; set; } = PlayStatus.Pending;

        public int? Rating { get; set; }

        public int HoursPlayed { get; set; } = 0;

        public string? ReviewNotes { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; } = null!;
    }
}
