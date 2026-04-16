using LowPolyBacklogApi.Data;
using LowPolyBacklogApi.DTOs.Game;
using LowPolyBacklogApi.Entities;
using LowPolyBacklogApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LowPolyBacklogApi.Repositories.Implementations
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Game> items, int totalCount)> GetAllAsync(GameQueryParameters parameters)
        {
            IQueryable<Game> query = _context.Games
                .Include(g => g.Genres);

            if (!string.IsNullOrWhiteSpace(parameters.Title))
            {
                query = query.Where(g => g.Title.ToLower().Contains(parameters.Title.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Genre))
            {
                query = query.Where(g => g.Genres.Any(gen => gen.Name.ToLower() == parameters.Genre.ToLower()));
            }

            if (parameters.Year.HasValue)
            {
                query = query.Where(g => g.ReleaseYear == parameters.Year);
            }

            int totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(g => g.Title) 
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return (items, totalCount);
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            var game = await _context.Games
                .Include(g => g.Genres)
                .Include(g => g.BacklogEntry)
                .FirstOrDefaultAsync(g => g.Id == id);

            return game;

        }

        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);

            await _context.SaveChangesAsync();
        }
    }
}
