using GameZone.DAL.Repositories.Interfaces;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DAL.Repositories.Implementations
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(GameZoneDbContext context) : base(context)
        {
        }

        public void Add(Game item)
        {
            foreach (var category in item.Categories)
                _context.Categories.Attach(category);

            base.Add(item);
        }

        public IQueryable<Game> Get()
        {
            return _context.Games.AsNoTracking();
        }
    }
}
