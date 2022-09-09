using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using GameZone.Service.Interfaces;
using GameZone.ViewModels;
using GameZone.ViewModels.GameVM;
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

        [HttpGet]
        public IActionResult CreateGame()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame(GameViewModel gameViewModel)
        {
            var game = new Game()
            {
                Name = gameViewModel.Name,
                Description = gameViewModel.Description,
                Price = gameViewModel.Price,
                ReleaseDate = gameViewModel.ReleaseDate,
                DeveloperId = gameViewModel.DeveloperId
            };

            var response = await _gameService.CreateGame(game);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
                return RedirectToAction("GetCars");

            return RedirectToAction("Error");
        }
    }
}
