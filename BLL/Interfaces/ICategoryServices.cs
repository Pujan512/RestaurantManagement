using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Category;

namespace RestaurantManagement.BLL.Interfaces
{
    public interface ICategoryServices
    {
        public Task<IEnumerable<CategoryVM>> GetCategoriesAsync();
        public Task<CategoryVM?> GetCategoryAsync(int id);
        public Task<CategoryVM?> GetCategoryWithItemsAsync(int id);
        public Task<bool> AddCategoryAsync(AddCategoryVM categoryVM);
        public Task<bool> DeleteCategoryAsync(int id);
        public Task<bool> EditCategoryAsync(int id, EditCategoryVM categoryVM);
        public EditCategoryVM EditVMMapper(CategoryVM categoryVM);
    }
}
