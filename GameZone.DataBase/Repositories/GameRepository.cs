using GameZone.DataBase.Interfaces;
using GameZone.Domain.Entities;
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

        public Task<Game> Create(Game item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Game>> Get()
        {
            return await _context.Games.Include(g => g.Developer).Include(g => g.Images).ToListAsync();
        }

        public Task<Game> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetAllCategories(int GameId)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetByName(string Name)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetCompany(int GameId)
        {
            throw new NotImplementedException();
        }
    }
}
