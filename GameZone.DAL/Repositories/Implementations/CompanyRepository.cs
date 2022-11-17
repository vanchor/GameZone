using GameZone.DAL.Repositories.Interfaces;
using GameZone.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DAL.Repositories.Implementations
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(GameZoneDbContext context) : base(context)
        {
        }
    }
}
