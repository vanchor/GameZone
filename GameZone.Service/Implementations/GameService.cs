using GameZone.DAL.Interfaces;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using GameZone.Domain.Core.Response;
using GameZone.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.Service.Implementations
{
    public class GameService : IGameService
    {
        private readonly IBaseRepository<Game> _gameRepository;
        public GameService(IBaseRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public BaseResponse<IEnumerable<Game>> GetGame(int id, bool includeDeveloper, ImageType imageType)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<IEnumerable<Game>> GetGame(string name, bool includeDeveloper, ImageType imageType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> SortGamesByDate(IEnumerable<Game> games)
        {
            var sortedGames = games.OrderByDescending(g => g.ReleaseDate);
            return sortedGames;
        }

        public BaseResponse<IEnumerable<Game>> GetGames(bool includeDeveloper = true, ImageType imageType = ImageType.fullSize)
        {
            try
            {
                var queryable = _gameRepository.Get();

                if (includeDeveloper)
                    queryable = queryable.Include(g => g.Developer);

                var games = queryable.Include(g => g.Images.Where(i => i.Type == imageType)).ToList();

                if (games.Count() == 0)
                {
                    return new BaseResponse<IEnumerable<Game>> {
                        Description = "0 items found",
                        StatusCode = HttpStatusCode.OK
                    };  
                }

                return new BaseResponse<IEnumerable<Game>>
                {
                    Data = games,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>()
                {
                    Description = $"[GetGames] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Game>> CreateGame(Game game)
        {
            try
            {
                await _gameRepository.Create(game);
                return new BaseResponse<Game>() { 
                    Data = game,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Game>(){
                    Description = $"[CreateGame] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteGame(int id)
        {
            try
            {
                var game = await _gameRepository.Get().FirstOrDefaultAsync(g => g.Id == id);
                if(game == null)
                {
                    return new BaseResponse<bool>() {
                        Description = "Game not found",
                        StatusCode = HttpStatusCode.NotFound,
                        Data = false
                    };
                }

                await _gameRepository.Delete(game);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>() {
                    Data = false,
                    Description = $"[DeleteGame] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
