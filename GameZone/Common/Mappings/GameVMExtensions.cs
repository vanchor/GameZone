using GameZone.Domain.Core.Entities;
using GameZone.ViewModels.GameVM;

namespace GameZone.Common.Mappings
{
    public static class GameVMExtensions
    {
        public static Game ToGame(this GameViewModel gameVM) => new Game
        {
            Name = gameVM.Name,
            Description = gameVM.Description,
            Price = gameVM.Price,
            ReleaseDate = gameVM.ReleaseDate,
            DeveloperId = gameVM.DeveloperId,
            Images = new List<Image>()
        };

        public static GameViewModel ToViewModel(this Game game)
        {

            var cat = new List<int>();
            foreach (var c in game.Categories)
                cat.Add(c.Id);

            var gameVM = new GameViewModel()
            {
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate,
                DeveloperId = game.DeveloperId,
                CategoriesId = cat
            };

            return gameVM;
        } 
    }
}

