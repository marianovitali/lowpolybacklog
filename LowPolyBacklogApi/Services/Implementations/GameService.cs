using AutoMapper;
using LowPolyBacklogApi.Data;
using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.Entities;
using LowPolyBacklogApi.Repositories.Interfaces;
using LowPolyBacklogApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LowPolyBacklogApi.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, ApplicationDbContext context, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GameResponseDto>> GetAllGamesAsync(string? title, string? genre, int? year)
        {
            var games = await _gameRepository.GetAllAsync(title, genre, year);

            return _mapper.Map<IEnumerable<GameResponseDto>>(games);

        }

        public async Task<GameResponseDto?> GetGameByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            return _mapper.Map<GameResponseDto>(game);
        }

        public async Task<GameResponseDto> CreateGameAsync(GameCreateDto game)
        {
            var newGame = _mapper.Map<Game>(game);

            var realGenres = await _context.Genres
                .Where(g => game.GenreIds
                .Contains(g.Id))
                .ToListAsync();

            newGame.Genres = realGenres;

            await _gameRepository.AddAsync(newGame);

            return _mapper.Map<GameResponseDto>(newGame);

        }
        public async Task<GameResponseDto> UpdateAsync(GameUpdateDto game, int id)
        {
            var existingGame = await _gameRepository.GetByIdAsync(id);
            if (existingGame == null)
            {
                throw new KeyNotFoundException($"The Game with the ID: {id} does not exist.");
            }

            _mapper.Map(game, existingGame);


            var newRealGenres = await _context.Genres
                .Where(g => game.GenreIds
                .Contains(g.Id))
                .ToListAsync();

            existingGame.Genres = newRealGenres;

            await _gameRepository.UpdateAsync(existingGame);

            return _mapper.Map<GameResponseDto>(existingGame);

        }

        public async Task DeleteAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);

            if (game == null)
            {
                throw new KeyNotFoundException($"The Game with the ID: {id} does not exist.");
            }

            await _gameRepository.DeleteAsync(game);
        }

    }
}
