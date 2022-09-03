using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.Domain.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int DeveloperId { get; set; }

        public virtual Company Developer { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
