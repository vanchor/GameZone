using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Response;
using GameZone.Service.Interfaces;
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

        public IViewComponentResult Invoke()
        {
            var response = _companyService.GetCompanies();
            return View(response.Data);
        }
    }
}
