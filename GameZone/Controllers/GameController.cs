﻿using GameZone.DataBase.Interfaces;
using GameZone.Domain.Enum;
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
            var allGames = await _gameRepository.Get(ImageType.medium);
            return View(allGames);
        }
    }
}