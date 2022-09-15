using GameZone.Domain.Core.Entities;
using GameZone.ViewModels.GameVM;

namespace GameZone.Common.Mappings
{
    public static class GameVMExtensions
    {
        public static Game ToGame(this GameViewModel GameVM) => new Game
        {
            Name = GameVM.Name,
            Description = GameVM.Description,
            Price = GameVM.Price,
            ReleaseDate = GameVM.ReleaseDate,
            DeveloperId = GameVM.DeveloperId,
            Images = new List<Image>()
        };
    }
}

