using System.Linq.Expressions;

namespace GameZone.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<T>
        where T : class
    {
        IQueryable<T> Get();
        void Add(T item);
        void AddRange(IEnumerable<T> items);
        T Update(T item);
        void Remove(T item);
        void RemoveRange(IEnumerable<T> entities);
        void SaveChanges();
        Task SaveChangesAsync();

        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    }
}
