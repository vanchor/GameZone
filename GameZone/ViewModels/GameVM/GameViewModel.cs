namespace GameZone.ViewModels.GameVM
{
    public class GameViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int DeveloperId { get; set; }

        public IFormFile[] photos { get; set; }
    }
}
