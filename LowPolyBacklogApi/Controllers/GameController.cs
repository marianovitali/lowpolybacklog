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
        private readonly IImageService _imageService;

        public GameController(IGameService gameService, IImageService imageService)
        {
            _gameService = gameService;
            _imageService = imageService;
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
        public async Task<ActionResult<GameResponseDto>> Create([FromForm] GameCreateDto gameDto)
        {
            if (gameDto.ImageFile != null)
            {
                var imageUrl = await _imageService.UploadImageAsync(gameDto.ImageFile);
                gameDto.CoverImageUrl = imageUrl;
            }


            var createdGame = await _gameService.CreateGameAsync(gameDto);

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
