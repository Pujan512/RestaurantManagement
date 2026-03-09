using RestaurantManagement.Core.Models;

namespace RestaurantManagement.DAL.Interfaces
{
    public interface ICategoryRepo
    {
        /*
        public Task<IEnumerable<Category>> GetCategoriesAsync();
        public Task<Category?> GetCategoryAsync(int id);
        public Task AddCategoryAsync(Category category);
        public Task EditCategoryAsync();
        public Task DeleteCategoryAsync(int id); */
        public Task<Category?> GetCategoryWithItemsAsync(int id);
    }
}
