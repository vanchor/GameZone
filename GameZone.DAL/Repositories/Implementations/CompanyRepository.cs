using GameZone.DAL.Interfaces;
using GameZone.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.DAL.Repositories.Implementations
{
    public class CompanyRepository : IBaseRepository<Company>
    {
        private readonly GameZoneDbContext _context;

        public CompanyRepository(GameZoneDbContext context)
        {
            _context = context;
        }

        public async Task Create(Company item)
        {
            _context.Companies.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Company item)
        {
            _context.Companies.Remove(item);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Company> Get()
        {
            return _context.Companies;
        }

        public async Task<Company> Update(Company item)
        {
            _context.Companies.Update(item);
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
