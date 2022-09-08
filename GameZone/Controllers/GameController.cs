using GameZone.Domain.Core.Enum;
using GameZone.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public IActionResult GetGames()
        {
            var response = _gameService.GetGames(imageType: ImageType.medium);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return View(response.Data);

            return RedirectToAction("Error");   
        }
    }
}
