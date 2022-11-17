using GameZone.DAL.Repositories.Interfaces;
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
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<BaseResponse<Company>> CreateCompany(Company company)
        {
            try
            {
                _companyRepository.Add(company);
                await _companyRepository.SaveChangesAsync();
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

        public async Task<BaseResponse<Company>> DeleteCompany(int id)
        {
            try
            {
                var company = await _companyRepository.Get().FirstOrDefaultAsync(x => x.Id == id);

                if (company == null)
                    return new BaseResponse<Company>() {
                        Description = "Company not found",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };

                _companyRepository.Remove(company);
                await _companyRepository.SaveChangesAsync();

                return new BaseResponse<Company>() {
                    Data = company,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<Company>() {
                    Description = $"[DeleteCompany] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<Company>>> GetCompanies(bool includeGames = false)
        {
            try
            {
                var queryable = _companyRepository.Get();
                if (includeGames)
                    queryable = queryable.Include(c => c.Games);

                var companies = await queryable.ToListAsync();

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

        public async Task<BaseResponse<Company>> GetCompany(int id, bool includeGames = false)
        {
            try
            {
                var queryable = _companyRepository.Get();
                if (includeGames)
                    queryable = queryable.Include(c => c.Games);

                var company = await queryable.FirstOrDefaultAsync(x => x.Id == id);

                if (company == null)
                    return new BaseResponse<Company>() {
                        Description = "Company not found",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };

                return new BaseResponse<Company>() {
                    Data = company,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Company>() { 
                    Description = $"[GetCompanyById] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Company>> UpdateCompany(Company company)
        {
            try
            {
                var companyinDb = await _companyRepository.Get().FirstOrDefaultAsync(x => x.Id == company.Id);
                if (companyinDb == null)
                    return new BaseResponse<Company>()
                    {
                        Description = "Company not found",
                        StatusCode = System.Net.HttpStatusCode.NotFound
                    };

                _companyRepository.Update(company);
                await _companyRepository.SaveChangesAsync();

                return new BaseResponse<Company>() {
                    Data = company,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Company>() {
                    Description = $"[UpdateCompany] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
