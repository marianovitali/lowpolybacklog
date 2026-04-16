using LowPolyBacklogApi.DTOs.Backlog;

namespace LowPolyBacklogApi.DTOs.Game
{
    public class GameDetailsResponseDto : GameResponseDto
    { 
        public BacklogResponseDto? BacklogInfo { get; set; }
    }
}
