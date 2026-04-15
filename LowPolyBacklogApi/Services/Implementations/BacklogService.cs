using AutoMapper;
using LowPolyBacklogApi.DTOs.Backlog;
using LowPolyBacklogApi.Entities;
using LowPolyBacklogApi.Repositories.Interfaces;
using LowPolyBacklogApi.Services.Interfaces;

namespace LowPolyBacklogApi.Services.Implementations
{
    public class BacklogService : IBacklogService
    {
        private readonly IBacklogRepository _backlogRepository;
        private readonly IMapper _mapper;

        public BacklogService(IBacklogRepository backlogRepository, IMapper mapper)
        {
            _backlogRepository = backlogRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BacklogResponseDto>> GetAllBacklogsAsync()
        {
            var entry = await _backlogRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<BacklogResponseDto>>(entry);
        }

        public async Task<BacklogResponseDto> GetBacklogByIdAsync(int id)
        {
            var entry = await _backlogRepository.GetByIdAsync(id);

            return _mapper.Map<BacklogResponseDto>(entry);
        }

        public async Task<BacklogResponseDto> CreateBacklogAsync(BacklogCreateDto backlogDto)
        {
            var newEntry = _mapper.Map<BacklogEntry>(backlogDto);

            await _backlogRepository.AddAsync(newEntry);

            return _mapper.Map<BacklogResponseDto>(newEntry);

        }

        public async Task<BacklogResponseDto> UpdateBacklogAsync(int id, BacklogUpdateDto backlogDto)
        {
            var existingEntry = await _backlogRepository.GetByIdAsync(id);

            if (existingEntry == null)
            {
                throw new KeyNotFoundException($"The entry with the ID: {id} does not exist.");
            }

            _mapper.Map(backlogDto, existingEntry);

            await _backlogRepository.UpdateAsync(existingEntry);

            return _mapper.Map<BacklogResponseDto>(existingEntry);
        }

        public async Task DeleteAsync(int id)
        {
            var backlog = await _backlogRepository.GetByIdAsync(id);

            if (backlog == null)
            {
                throw new KeyNotFoundException($"The entry with the ID: {id} does not exist.");

            }

            await _backlogRepository.DeleteAsync(backlog);
        }
    }
}
