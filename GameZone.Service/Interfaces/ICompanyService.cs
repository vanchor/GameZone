using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Response;

namespace GameZone.Service.Interfaces
{
    public interface ICompanyService
    {
        Task<BaseResponse<IEnumerable<Company>>> GetCompanies (bool includeGames = false);
        Task<BaseResponse<Company>> GetCompany(int id, bool includeGames = false);
        Task<BaseResponse<Company>> CreateCompany (Company company);
        Task<BaseResponse<Company>> UpdateCompany (Company company);
        Task<BaseResponse<Company>> DeleteCompany (int id);
    }
}
