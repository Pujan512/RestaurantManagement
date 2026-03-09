using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Models;
using RestaurantManagement.DAL.Data;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.DAL.Repositories
{
    public class CategoryRepo(RestaurantDBContext context) : ICategoryRepo
    {
        private readonly RestaurantDBContext _context = context;
        /*
        public async Task AddCategoryAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id);
            if (category == null)
            {
                throw new Exception("Blog Not Found");
            }
            _context.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditCategoryAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Category
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
        } */

        public async Task<Category?> GetCategoryWithItemsAsync(int id)
        {
            return await _context.Category.Include(c => c.MenuItems)
                                          .FirstOrDefaultAsync(c => c.Id == id);

        }
    }
}
