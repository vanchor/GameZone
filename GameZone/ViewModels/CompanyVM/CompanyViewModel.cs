using GameZone.Domain.Core.Entities;

namespace GameZone.ViewModels.CompanyVM
{
    public class CompanyViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile CompanyLogo { get; set; }
    }
}
