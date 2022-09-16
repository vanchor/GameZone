using GameZone.Common;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Response;
using GameZone.Service.Interfaces;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Components
{
    public class ListOfDevelopers : ViewComponent
    {
        private readonly ICompanyService _companyService;
        public ListOfDevelopers(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _companyService.GetCompanies();
            return View(response.Data);
        }
    }
}
