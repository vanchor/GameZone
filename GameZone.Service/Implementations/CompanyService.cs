using GameZone.DAL.Interfaces;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Response;
using GameZone.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.Service.Implementations
{
    public class CompanyService : ICompanyService
    {
        private readonly IBaseRepository<Company> _companyRepository;

        public CompanyService(IBaseRepository<Company> companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse<Company>> CreateCompany(Company company)
        {
            try
            {
                await _companyRepository.Create(company);
                return new BaseResponse<Company>() {
                    Data = company,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<Company>() {
                    Description = $"[CreateCompany] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<IEnumerable<Company>> GetCompanies(bool includeGames = false)
        {
            try
            {
                var queryable = _companyRepository.Get();
                if (includeGames)
                    queryable = queryable.Include(c => c.Games);

                var companies = queryable.ToList();

                if(companies == null)
                    return new BaseResponse<IEnumerable<Company>>() { 
                        Description = "0 companies was found",
                        StatusCode = System.Net.HttpStatusCode.OK
                    };

                return new BaseResponse<IEnumerable<Company>>(){
                    Data = companies,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Company>>()
                {
                    Description = $"[GetCompanies] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
