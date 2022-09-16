using GameZone.Common;
using GameZone.Domain.Core.Entities;
using GameZone.Service.Interfaces;
using GameZone.ViewModels.CompanyVM;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public IActionResult ReloadDevelopersList()
        {
            return ViewComponent("ListOfDevelopers");
        }

        public IActionResult GetCompanies()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany(CompanyViewModel companyVM)
        {
            try
            {
                var path = Path.Combine(FileHelper.FullPathToMedaiFolder("Companies"), companyVM.CompanyLogo.FileName);
                await FileHelper.UploadFile(companyVM.CompanyLogo, path);

                var company = new Company()
                {
                    Name = companyVM.Name,
                    Description = companyVM.Description,
                    LogoUrl = companyVM.CompanyLogo.FileName
                };

                var response = await _companyService.CreateCompany(company);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return View();
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
    }
}
