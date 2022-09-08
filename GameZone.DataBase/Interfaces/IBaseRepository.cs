using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DataBase.Interfaces
{
    public interface IBaseRepository<T>
        where T : class
    {
        IQueryable<T> Get();
        Task Create(T item);
        Task<T> Update(T item);
        Task Delete(T item);
    }
}
