using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.Filters;
using LowPolyBacklogApi.Helpers;
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
        public async Task<ActionResult<PagedResponse<GameResponseDto>>> GetAll([FromQuery] GameQueryParameters parameters)
        {
            var (gamesDto, totalCount) = await _gameService.GetAllGamesAsync(parameters);

            var response = new PagedResponse<GameResponseDto>
            {
                Items = gamesDto,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalItems = totalCount
            };

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GameDetailsResponseDto>> GetById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);

            if (game is null)
            {
                return NotFound(new { message = $"Game with ID: {id} not found." });
            }

            return Ok(game);
        }

        [ApiKeyAuth]
        [HttpPost]
        public async Task<ActionResult<GameResponseDto>> Create([FromBody] GameCreateDto game)
        {
            var createdGame = await _gameService.CreateGameAsync(game);

            return CreatedAtAction(nameof(GetById), new { id = createdGame.Id }, createdGame);
        }

        [ApiKeyAuth]
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

        [ApiKeyAuth]
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
