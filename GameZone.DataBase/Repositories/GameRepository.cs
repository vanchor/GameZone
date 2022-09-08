using GameZone.DataBase.Interfaces;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DataBase.Repositories
{
    public class GameRepository : IBaseRepository<Game>
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

        public IQueryable<Game> Get()
        {
            return _context.Games;
        }

        public async Task<Game> Update(Game item)
        {
            _context.Games.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task Delete(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }
    }
}
