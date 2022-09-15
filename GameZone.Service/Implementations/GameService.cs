using GameZone.DAL.Interfaces;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using GameZone.Domain.Core.Response;
using GameZone.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<BaseResponse<Game>> GetGame(int id, bool includeDeveloper = true, ImageType imageType = ImageType.fullSize)
        {
            try
            {
                var queryable = _gameRepository.Get();

                if (includeDeveloper)
                    queryable = queryable.Include(g => g.Developer);
                
                var game = await queryable.Include(g => g.Images.Where(i => i.Type == imageType)).FirstOrDefaultAsync(x => x.Id == id);

                if(game == null)
                    return new BaseResponse<Game>() { 
                        Description = "Game not found",
                        StatusCode = HttpStatusCode.NotFound
                    };

                return new BaseResponse<Game>() {
                    Data = game,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Game>() { 
                    Description = $"[GetGameById] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Game>>> GetGame(string name, bool includeDeveloper = false, ImageType imageType = ImageType.fullSize)
        {
            try
            {
                var queryable = _gameRepository.Get();

                if (includeDeveloper)
                    queryable = queryable.Include(g => g.Developer);

                var games = await queryable.Include(g => g.Images.Where(i => i.Type == imageType))
                                            .Where(g => EF.Functions.Like(g.Name, $"%{name}%"))
                                            .ToListAsync();

                if (games == null)
                    return new BaseResponse<IEnumerable<Game>>()
                    {
                        Description = "Games not found",
                        StatusCode = HttpStatusCode.NotFound
                    };

                return new BaseResponse<IEnumerable<Game>>()
                {
                    Data = games,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Game>>()
                {
                    Description = $"[GetGameById] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Game>>> GetGames(int? count = null,
                                                        bool includeDeveloper = true,
                                                        ImageType imageType = ImageType.fullSize,
                                                        Expression<Func<Game, object>> sorter = null)
        {
            try
            {
                var queryable = _gameRepository.Get();

                if (sorter != null)
                    queryable = queryable.OrderByDescending(sorter);

                if (count != null && count > 0 && count < queryable.Count())
                    queryable = queryable.Take((int)count);

                if (includeDeveloper)   
                    queryable = queryable.Include(g => g.Developer);
 
                var games = await queryable.Include(g => g.Images.Where(i => i.Type == imageType)).ToListAsync();

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

        public async Task<BaseResponse<Game>> UpdateGame(Game game)
        {
            try
            {
                var gameInDB = await _gameRepository.Get().FirstOrDefaultAsync(x => x.Id == game.Id);
                if(gameInDB == null)
                    return new BaseResponse<Game>() {
                        Description = "Game not found",
                        StatusCode = HttpStatusCode.OK
                    };
                
                await _gameRepository.Update(game);
                return new BaseResponse<Game>() { 
                    StatusCode = HttpStatusCode.OK,
                    Data = game
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Game>() { 
                    Description = $"[UpdateGane] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<int>> GetLastId()
        {
            try
            {
                int lastID = await _gameRepository.Get().OrderByDescending(x => x.Id).Select(x => x.Id).FirstAsync();

                return new BaseResponse<int>{
                    Data = lastID,
                    StatusCode = HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>(){
                    Description = $"[GetLastId] : {ex.Message}",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
