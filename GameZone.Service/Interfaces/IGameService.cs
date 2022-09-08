using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameZone.Domain.Entities;
using GameZone.Domain.Response;


namespace GameZone.Service.Interfaces
{
    internal interface IGameService
    {
        Task<BaseResponse<IEnumerable<Game>>> GetGames();
    }
}
