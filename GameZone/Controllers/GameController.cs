using GameZone.Common;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using GameZone.Service.Interfaces;
using GameZone.ViewModels;
using GameZone.ViewModels.GameVM;
using Microsoft.AspNetCore.Mvc;
using GameZone.Common.Mappings;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult ReloadDevelopersList()
        {
            return ViewComponent("ListOfDevelopers");
        }

        public async Task<IActionResult> GetGames()
        {
            var viewModel = new GetGamesViewModel();
            var response1 = await _gameService.GetGames(imageType: ImageType.medium);
            var response2 = await _gameService.GetGames(count: 5, includeDeveloper: false, sorter: x => x.ReleaseDate);
            if (response1.StatusCode == System.Net.HttpStatusCode.OK && 
                response2.StatusCode == System.Net.HttpStatusCode.OK )
            {
                viewModel.GamesWithSmallPictures = response1.Data;
                viewModel.GamesForCarousel = response2.Data;
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
            var game = gameViewModel.ToGame();

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
