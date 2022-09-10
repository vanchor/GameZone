using GameZone.Common;
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
        [RequestSizeLimit(100_000_000)]
        public async Task<IActionResult> CreateGame(GameViewModel gameViewModel, List<IFormFile> photos)
        {
            var game = new Game()
            {
                Name = gameViewModel.Name,
                Description = gameViewModel.Description,
                Price = gameViewModel.Price,
                ReleaseDate = gameViewModel.ReleaseDate,
                DeveloperId = gameViewModel.DeveloperId,
                Images = new List<Image>()
            };
            var nameWithoutSpaces = string.Join("_", gameViewModel.Name.Split(Path.GetInvalidFileNameChars()));
            var fullPathToGameFolder = FileHelper.FullPathToMedaiFolder(nameWithoutSpaces);
            FileHelper.CreateDirectory(fullPathToGameFolder);
            try
            {
                foreach (IFormFile photo in photos)
                {
                    var path = Path.Combine(nameWithoutSpaces, photo.FileName);
                    game.Images.Add(new Image()
                    {
                        Url = path,
                        Title = nameWithoutSpaces,
                        Type = ImageType.fullSize
                    });

                    await FileHelper.UploadFile(photo, Path.Combine(fullPathToGameFolder, photo.FileName));

                    // Resize image to 400x225
                    path = path + "400_225.jpg";
                    game.Images.Add(new Image()
                    {
                        Url = path,
                        Title = nameWithoutSpaces,
                        Type = ImageType.medium
                    });

                    FileHelper.ResizeImage(photo, Path.Combine(fullPathToGameFolder, photo.FileName+"400_225.jpg"), 400, 225);
                }

                var response = await _gameService.CreateGame(game);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return RedirectToAction("GetGames");
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
    }
}
