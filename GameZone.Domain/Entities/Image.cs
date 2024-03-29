﻿using GameZone.Domain.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.Domain.Core.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }

        public int GameId { get; set; }

        public ImageType Type { get; set; }

        public virtual Game game { get; set; }
    }
}
    