using GameZone.Domain.Core.Enum;
using GameZone.Service.Interfaces;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult GetGames()
        {
            var viewModel = new GetGamesViewModel();
            var response = _gameService.GetGames(imageType: ImageType.medium);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                viewModel.GamesWithSmallPictures = response.Data;

                response = _gameService.GetGames(includeDeveloper: false);
                viewModel.GamesForCarousel = _gameService.SortGamesByDate(response.Data);
                return View(viewModel);
            }
                

            return RedirectToAction("Error");   
        }
    }
}
