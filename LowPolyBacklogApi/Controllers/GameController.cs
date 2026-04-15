using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LowPolyBacklogApi.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameResponseDto>>> GetAll(string? title, string? genre, int? year)
        {
            var games = await _gameService.GetAllGamesAsync(title, genre, year);
            return Ok(games);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GameResponseDto>> GetById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);

            if (game is null)
            {
                return NotFound(new { message = $"Game with ID: {id} not found." });
            }

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameResponseDto>> Create([FromBody] GameCreateDto game)
        {
            var createdGame = await _gameService.CreateGameAsync(game);

            return CreatedAtAction(nameof(GetById), new { id = createdGame.Id }, createdGame);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<GameResponseDto>> Update(int id, [FromBody] GameUpdateDto game)
        {
            try
            {
                var updatedGame = await _gameService.UpdateAsync(game, id);
                return Ok(updatedGame);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _gameService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

    }
}
