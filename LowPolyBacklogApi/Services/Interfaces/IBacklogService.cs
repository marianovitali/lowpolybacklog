using LowPolyBacklogApi.DTOs.Backlog;
using LowPolyBacklogApi.Entities;

namespace LowPolyBacklogApi.Services.Interfaces
{
    public interface IBacklogService
    {
        Task<IEnumerable<BacklogResponseDto>> GetAllBacklogsAsync();
        Task<BacklogResponseDto> GetBacklogByIdAsync(int id);
        Task<BacklogResponseDto> CreateBacklogAsync(BacklogCreateDto backlogDto);
        Task<BacklogResponseDto> UpdateBacklogAsync(int id, BacklogUpdateDto backlogDto);
        Task DeleteAsync(int id);


    }
}
