using GameZone.DAL.Interfaces;
using GameZone.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DAL.Repositories.Implementations
{
    internal class ImageRepository : IBaseRepository<Image>
    {
        private readonly GameZoneDbContext _context;

        public ImageRepository(GameZoneDbContext context)
        {
            _context = context;
        }

        public async Task Create(Image item)
        {
            _context.Images.Add(item);
            await _context.SaveChangesAsync();
        }

        public Task Delete(Image item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Image> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Image> Update(Image item)
        {
            throw new NotImplementedException();
        }
    }
}
