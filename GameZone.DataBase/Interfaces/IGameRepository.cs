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
        Task<IEnumerable<Game>> Get(ImageType image);
        Task<Game> Get(int id, ImageType image);
        Task<Company> GetCompany(int GameId);
    }
}
