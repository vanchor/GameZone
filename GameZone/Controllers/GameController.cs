using GameZone.Common;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Enum;
using GameZone.Service.Interfaces;
using GameZone.ViewModels;
using GameZone.ViewModels.GameVM;
using Microsoft.AspNetCore.Mvc;
using GameZone.Common.Mappings;
using System.Net;

namespace GameZone.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> GetGames()
        {
            var viewModel = new GetGamesViewModel();
            var response1 = await _gameService.GetGames(imageType: ImageType.medium);
            var response2 = await _gameService.GetGames(count: 5, includeDeveloper: false, sorter: x => x.ReleaseDate);
            if (response1.StatusCode == HttpStatusCode.OK &&
                response2.StatusCode == HttpStatusCode.OK)
            {
                viewModel.GamesWithSmallPictures = response1.Data;
                viewModel.GamesForCarousel = response2.Data;
                return View(viewModel);
            }


            return RedirectToAction("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _gameService.DeleteGame(id);

            if (response.StatusCode == HttpStatusCode.OK) {
                var path = FileHelper.FullPathToMedaiFolder($"Games\\{id}");
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                return RedirectToAction("GetGames");
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
            if (ModelState.IsValid)
            {
                var game = gameViewModel.ToGame();
                var response = await _gameService.CreateGame(game);
                try
                {
                    if (photos != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        var fullPathToGameFolder = FileHelper.FullPathToMedaiFolder($"Games\\{game.Id}");
                        FileHelper.CreateDirectory(fullPathToGameFolder);

                        foreach (IFormFile photo in photos)
                        {
                            game.Images.Add(new Image()
                            {
                                Url = $"{game.Id}\\{photo.FileName}",
                                Title = game.Name,
                                Type = ImageType.fullSize,
                            });
                            await FileHelper.UploadFile(photo, Path.Combine(fullPathToGameFolder, photo.FileName));

                            // Resize image to 400x225
                            game.Images.Add(new Image()
                            {
                                Url = $"{game.Id}\\{photo.FileName}\\400_225.jpg",
                                Title = game.Name,
                                Type = ImageType.medium
                            });
                            FileHelper.ResizeImage(photo, Path.Combine(fullPathToGameFolder, photo.FileName + "400_225.jpg"), 400, 225);
                        }

                        response = await _gameService.UpdateGame(game);
                    }

                    if (response.StatusCode == HttpStatusCode.OK)
                        return RedirectToAction("GetGames");
                    return RedirectToAction("Error");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Error");
                }
            }
            return View(gameViewModel); 
        }
    }
}
