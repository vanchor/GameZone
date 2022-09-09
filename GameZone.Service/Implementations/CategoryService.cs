using GameZone.DAL.Interfaces;
using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Response;
using GameZone.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameZone.Service.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        public CategoryService(IBaseRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Task<BaseResponse<Category>> CreateCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<bool>> DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Category>> Edit(int id, Category model)
        {
            throw new NotImplementedException();
        }

        public BaseResponse<IEnumerable<Category>> GetCategories()
        {
            try
            {
                var categories = _categoryRepository.Get().ToList();

                if(categories.Count == 0)
                {
                    return new BaseResponse<IEnumerable<Category>>() {
                        Description = "0 categories found",
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }

                return new BaseResponse<IEnumerable<Category>>() {
                    Data = categories,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Category>>() {
                    Description = $"[GetCategories] : {ex.Message}",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public Task<BaseResponse<Category>> GetCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Category>> GetCategory(string name)
        {
            throw new NotImplementedException();
        }
    }
}
