using GameZone.DAL.Interfaces;
using GameZone.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DAL.Repositories
{
    public class CategoryRepository : IBaseRepository<Category>
    {
        public readonly GameZoneDbContext _context;

        public CategoryRepository(GameZoneDbContext context)
        {
            _context = context;
        }

        public async Task Create(Category item)
        {
            _context.Categories.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Category item)
        {
            _context.Categories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Category> Get()
        {
           return _context.Categories;
        }

        public async Task<Category> Update(Category item)
        {
            _context.Categories.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
