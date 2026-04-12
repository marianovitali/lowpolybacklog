using LowPolyBacklogApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace LowPolyBacklogApi.DTOs.Backlog
{
    public class BacklogCreateDto
    {
        [Required(ErrorMessage = "Status title is required.")] 
        public PlayStatus Status { get; set; } = PlayStatus.Pending;

        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public int? Rating { get; set; }

        [Range(0, 9999, ErrorMessage = "Invalid hours played.")]
        public int HoursPlayed { get; set; } = 0;

        [MaxLength(500, ErrorMessage = "ReviewNotes cannot exceed 1000 characters.")]
        public string? ReviewNotes { get; set; }
        public required int GameId { get; set; }

    }
}
