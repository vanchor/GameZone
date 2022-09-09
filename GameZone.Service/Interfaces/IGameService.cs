using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using GameZone.Domain.Core.Response;


namespace GameZone.Service.Interfaces
{
    public interface IGameService
    {
        BaseResponse<IEnumerable<Game>> GetGames(
            bool includeDeveloper = true, 
            ImageType imageType = ImageType.fullSize);
        BaseResponse<IEnumerable<Game>> GetGame(int id, 
            bool includeDeveloper = true, 
            ImageType imageType = ImageType.fullSize);
        BaseResponse<IEnumerable<Game>> GetGame(string name, 
            bool includeDeveloper = true, 
            ImageType imageType = ImageType.fullSize);
        IEnumerable<Game> SortGamesByDate(IEnumerable<Game> games);

        Task<BaseResponse<Game>> CreateGame(Game game);
        Task<BaseResponse<bool>> DeleteGame(int id);

    }
}
