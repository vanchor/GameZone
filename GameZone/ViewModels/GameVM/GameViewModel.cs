using GameZone.Domain.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels.GameVM
{
    public class GameViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.01, 200)]
        public decimal Price { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }   
        [Required]
        public int DeveloperId { get; set; }

        public List<IFormFile> photos { get; set; }

        public List<int> CategoriesId { get; set; }
    }
}
