﻿using Microsoft.EntityFrameworkCore;
using GameZone.Domain.Core.Entities;

namespace GameZone.DAL
{
    public class GameZoneDbContext : DbContext
    {
        public GameZoneDbContext(DbContextOptions<GameZoneDbContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}