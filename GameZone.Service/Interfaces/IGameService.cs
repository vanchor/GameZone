using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using GameZone.Domain.Core.Response;
using System.Linq.Expressions;

namespace GameZone.Service.Interfaces
{
    public interface IGameService
    {
        Task<BaseResponse<IEnumerable<Game>>> GetGames( int? count = null, 
                                                    bool includeDeveloper = true, 
                                                    ImageType imageType = ImageType.fullSize,
                                                    Expression<Func<Game, object>> sorter = null);

        Task<BaseResponse<Game>> GetGame(int id, 
            bool includeDeveloper = true, 
            ImageType imageType = ImageType.fullSize);
        Task<BaseResponse<IEnumerable<Game>>> GetGame(string name, 
            bool includeDeveloper = true, 
            ImageType imageType = ImageType.fullSize);

        Task<BaseResponse<Game>> CreateGame(Game game);
        Task<BaseResponse<bool>> DeleteGame(int id);
        Task<BaseResponse<Game>> UpdateGame(Game game);
    }
}
