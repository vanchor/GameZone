using GameZone.DataBase.Interfaces;
using GameZone.Domain.Entities;
using GameZone.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DataBase.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly GameZoneDbContext _context;

        public GameRepository(GameZoneDbContext context)
        {
            _context = context;
        }

        public async Task Create(Game item)
        {
            await _context.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> Get()
        {
            return await _context.Games.Include(g => g.Developer)
                                        .Include(g => g.Images)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<Game>> Get(ImageType type = ImageType.fullSize)
        {
            return await _context.Games.Include(g => g.Developer)
                                        .Include(g => g.Images.Where(i => i.Type == type))
                                        .ToListAsync();
        }

        public async Task<Game> Get(int id)
        {
            return await _context.Games.Include(g => g.Developer)
                                        .Include(g => g.Images)
                                        .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Game> Get(int id, ImageType type = ImageType.fullSize)
        {
            return await _context.Games.Include(g => g.Developer)
                                        .Include(g => g.Images.Where(i => i.Type == type))
                                        .FirstOrDefaultAsync(g => g.Id == id);
        }

        public Task<IEnumerable<Category>> GetAllCategories(int GameId)
        {
            throw new NotImplementedException();
        }

        public async Task<Game> GetByName(string Name)
        {
            return await _context.Games.Include(g => g.Developer)
                                       .Include(g => g.Images)
                                       .FirstOrDefaultAsync(g => g.Name == Name);
        }

        public Task<Company> GetCompany(int GameId)
        {
            throw new NotImplementedException();
        }
    }
}
