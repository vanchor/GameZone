﻿using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModels.GameVM
{
    public class GameViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public int DeveloperId { get; set; }

        public IFormFile[] photos { get; set; }
    }
}
