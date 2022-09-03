using GameZone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DataBase.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game>
    {
        Task<Game> GetByName(string Name);
        Task<IEnumerable<Category>> GetAllCategories(int GameId);
        Task<Company> GetCompany(int GameId);
    }
}
