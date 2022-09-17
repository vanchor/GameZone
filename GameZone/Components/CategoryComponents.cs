using GameZone.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Components
{
    public class CategoryComponents : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryComponents(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(bool inOptions = false)
        {
            var response = await _categoryService.GetCategories();
            if (!inOptions)
                return View(response.Data);
            return View("_CategoriesSelection", response.Data);
        }
    }
}
