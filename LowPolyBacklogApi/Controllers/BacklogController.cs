using LowPolyBacklogApi.DTOs.Backlog;
using LowPolyBacklogApi.Entities;
using LowPolyBacklogApi.Services.Implementations;
using LowPolyBacklogApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LowPolyBacklogApi.Controllers
{
    [Route("api/backlogs")]
    [ApiController]
    public class BacklogController : ControllerBase
    {
        private readonly IBacklogService _backlogService;

        public BacklogController(IBacklogService backlogService)
        {
            _backlogService = backlogService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BacklogResponseDto>>> GetAll()
        {
            var entries = await _backlogService.GetAllBacklogsAsync();

            return Ok(entries);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BacklogResponseDto>> GetById(int id)
        {
            var entry = await _backlogService.GetBacklogByIdAsync(id);

            if (entry is null)
            {
                return NotFound(new { message = $"Entry with ID: {id} not found." });
            }

            return Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult<BacklogResponseDto>> Create([FromBody] BacklogCreateDto entry)
        {
            var createdEntry = await _backlogService.CreateBacklogAsync(entry);

            return CreatedAtAction(nameof(GetById), new { id = createdEntry.Id }, createdEntry);
        }

        
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BacklogResponseDto>> Update(int id, [FromBody] BacklogUpdateDto entry)
        {
            try
            {
                var updatedEntry = await _backlogService.UpdateBacklogAsync(id, entry);
                return Ok(updatedEntry);
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
                await _backlogService.DeleteAsync(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
