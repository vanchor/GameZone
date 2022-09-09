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
        private readonly IWebHostEnvironment _appEnvironment;

        public GameController(IGameService gameService, IWebHostEnvironment appEnvironment)
        {
            _gameService = gameService;
            _appEnvironment = appEnvironment;
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
        public async Task<IActionResult> CreateGame(GameViewModel gameViewModel, IFormFile[] photos)
        {
            ICollection<Image> images = new List<Image>();
            var nameWithoutSpaces = gameViewModel.Name.Trim().Replace(" ", "_");

            var pathToImagesFolder = Path.Combine(_appEnvironment.WebRootPath, 
                                                    "images", "Media", nameWithoutSpaces);

            foreach (IFormFile photo in photos)
            {
                var path = Path.Combine("Media", nameWithoutSpaces, photo.FileName);
                images.Add(new Image()
                {
                    Url = path,
                    Title = nameWithoutSpaces,
                    Type = ImageType.fullSize
                });
            }

            var game = new Game()
            {
                Name = gameViewModel.Name,
                Description = gameViewModel.Description,
                Price = gameViewModel.Price,
                ReleaseDate = gameViewModel.ReleaseDate,
                DeveloperId = gameViewModel.DeveloperId,
                Images = images
            };

            var response = await _gameService.CreateGame(game);

            DirectoryInfo di;
            try
            {
                // Determine whether the directory not exists.
                if (!Directory.Exists(pathToImagesFolder))
                {
                    // Try to create the directory.
                    di = Directory.CreateDirectory(pathToImagesFolder);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    foreach (IFormFile photo in photos)
                    {
                        var path = Path.Combine(pathToImagesFolder, photo.FileName);
                        var stream = new FileStream(path, FileMode.Create);
                        photo.CopyToAsync(stream);
                    }
                    return RedirectToAction("GetGames");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

            return RedirectToAction("Error");
        }
    }
}
