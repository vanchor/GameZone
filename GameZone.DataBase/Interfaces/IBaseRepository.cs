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
        Task<IEnumerable<T>> Get();
        Task<T> Get(int id);
        Task Create(T item);
        //Task Update(T item);
        Task Delete(T item);
    }
}
