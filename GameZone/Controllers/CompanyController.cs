using GameZone.Common;
using GameZone.Domain.Core.Entities;
using GameZone.Service.Interfaces;
using GameZone.ViewModels.CompanyVM;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(companyVM.CompanyLogo.FileName);
                var path = Path.Combine(FileHelper.FullPathToMedaiFolder("Companies"), newFileName);
                await FileHelper.UploadFile(companyVM.CompanyLogo, path);

                var company = new Company()
                {
                    Name = companyVM.Name,
                    Description = companyVM.Description,    
                    LogoUrl = newFileName
                };

                var response = await _companyService.CreateCompany(company);
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("GetCompanies");
                return RedirectToAction("Error");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public async Task<IActionResult> DeleteCompany(int id)
        {
            var response = await _companyService.DeleteCompany(id);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var path = FileHelper.FullPathToMedaiFolder($"Companies\\{response.Data.LogoUrl}");
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                return RedirectToAction("GetCompanies");
            }
            return RedirectToAction("Error");
        }
    }
}
