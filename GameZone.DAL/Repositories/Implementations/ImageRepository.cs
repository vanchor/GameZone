using GameZone.DAL.Repositories.Interfaces;
using GameZone.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DAL.Repositories.Implementations
{
    public class ImageRepository : BaseRepository<Image>
    {
        public ImageRepository(GameZoneDbContext context) : base(context)
        {
        }
    }
}
