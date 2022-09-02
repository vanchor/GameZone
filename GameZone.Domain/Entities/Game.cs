﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.Domain.Entities
{
    internal class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int DeveloperId { get; set; }


        public virtual Company Developer { get; set; }
        public virtual ICollection<Category> MyProperty { get; set; }
    }
}
