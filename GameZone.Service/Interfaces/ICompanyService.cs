using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Response;

namespace GameZone.Service.Interfaces
{
    public interface ICompanyService
    {
        BaseResponse<IEnumerable<Company>> GetCompanies (bool includeGames = false);
        Task<BaseResponse<Company>> CreateCompany (Company company);
    }
}
