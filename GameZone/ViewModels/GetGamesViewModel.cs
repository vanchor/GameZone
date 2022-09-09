using GameZone.Domain.Core.Entities;

namespace GameZone.ViewModels
{
    public class GetGamesViewModel
    {
        public IEnumerable<Game> GamesWithSmallPictures { get; set; }
        public IEnumerable<Game> GamesForCarousel { get; set; }
    }
}
