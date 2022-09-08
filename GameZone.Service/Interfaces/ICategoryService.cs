using GameZone.Domain.Core.Entities;
using GameZone.Domain.Core.Response;

namespace GameZone.Service.Interfaces
{
    public interface ICategoryService
    {
        BaseResponse<IEnumerable<Category>> GetCategories();
        Task<BaseResponse<Category>> GetCategory(int categoryId);
        Task<BaseResponse<Category>> GetCategory(string name);
        Task<BaseResponse<Category>> CreateCategory(Category category);
        Task<BaseResponse<bool>> DeleteCategory(int id);
        Task<BaseResponse<Category>> Edit(int id, Category model);

    }
}
