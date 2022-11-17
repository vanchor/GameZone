using GameZone.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DAL.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly GameZoneDbContext _context;

        public BaseRepository(GameZoneDbContext context)
        {
            _context = context;
        }

        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            _context.Set<T>().AddRange(items);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>();
        }

        public void Remove(T item)
        {
            _context.Set<T>().Remove(item);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public T Update(T item)
        {
            _context.Set<T>().Update(item);
            return item;
        }
    }
}
