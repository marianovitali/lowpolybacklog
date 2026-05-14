using LowPolyBacklogApi.DTOs.Igdb;
using LowPolyBacklogApi.Filters;
using LowPolyBacklogApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LowPolyBacklogApi.Controllers
{
    [ApiController]
    [Route("api/igdb")]
    public class IgdbController : ControllerBase
    {
        private readonly IIgdbService _igdbService;
        private readonly IGameService _gameService;

        public IgdbController(IIgdbService igdbService, IGameService gameService)
        {
            _igdbService = igdbService;
            _gameService = gameService;
        }

        
        [HttpGet("search")]
        public async Task<ActionResult<List<IgdbSearchResultDto>>> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { message = "El texto de búsqueda no puede estar vacío." });
            }

            var results = await _igdbService.SearchGamesAsync(query);
            return Ok(results);
        }

        [ApiKeyAuth] 
        [HttpPost("import")]
        public async Task<ActionResult> Import([FromBody] IgdbSearchResultDto selectedGame)
        {
            try
            {
                var createdGame = await _gameService.ImportFromIgdbAsync(selectedGame);

                return CreatedAtAction(
                    actionName: "GetById",
                    controllerName: "Game",
                    routeValues: new { id = createdGame.Id },
                    value: createdGame);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error al importar el juego: {ex.Message}" });
            }
        }
    }
}
