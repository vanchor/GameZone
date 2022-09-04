using GameZone.DataBase.Interfaces;
using GameZone.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var allGames = await _gameRepository.GetWithImageType(ImageType.medium);
            return View(allGames);
        }
    }
}
