using GameZone.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Components
{
    public class CategoryMenu : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryMenu(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var response = _categoryService.GetCategories();
            return View(response.Data);           
        }
    }
}
