using GameZone.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GameZone.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public IActionResult GetCategories()
        {
            var response = _categoryService.GetCategories();
            if(response.StatusCode == HttpStatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }
    }
}
