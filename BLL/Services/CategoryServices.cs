using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Category;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.BLL.Services
{
    public class CategoryServices(ICategoryRepo categoryRepo, IMapper mapper, IGenericRepository<Category> genericCategoryRepo) : ICategoryServices
    {
        private readonly ICategoryRepo _categoryRepo = categoryRepo;
        private readonly IGenericRepository<Category> _genericCategoryRepo = genericCategoryRepo;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> AddCategoryAsync(AddCategoryVM addCategoryVM)
        {
            Category category = new()
            {
                Title = addCategoryVM.Title
            };

            try
            {
                await _genericCategoryRepo.AddAsync(category);
                return true;
            }
            catch
            {
                throw new ArgumentException("Unable to add Category");
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _genericCategoryRepo.GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");

            try
            {
                await _genericCategoryRepo.DeleteAsync(id);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EditCategoryAsync(int id, EditCategoryVM editCategoryVM)
        {
            var category = await _genericCategoryRepo.GetByIdAsync(id);
            if (category == null) throw new Exception("Category not found");
            if (id != editCategoryVM.Id) throw new Exception("Category id doesn't match");

            category.Title = editCategoryVM.Title;
            try
            {
                await _genericCategoryRepo.EditAsync();
                return true;
            }
            catch
            {
                throw new Exception("Unable to update category in db");
            }

        }

        public async Task<IEnumerable<CategoryVM>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _genericCategoryRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<CategoryVM>>(categories);
            } catch
            {
                throw new Exception("Unable to get categories from db");
            }
        }

        public async Task<CategoryVM?> GetCategoryAsync(int id)
        {
            try
            {
                var category = await _genericCategoryRepo.GetByIdAsync(id);
                return _mapper.Map<CategoryVM>(category);
            }
            catch
            {
                throw new Exception("Unable to get category from db");
            }
        }

        public async Task<CategoryVM?> GetCategoryWithItemsAsync(int id)
        {
            try
            {
                var categoryWithItems = await _categoryRepo.GetCategoryWithItemsAsync(id);
                return _mapper.Map<CategoryVM>(categoryWithItems);
            }
            catch
            {
                throw new Exception("Unable to get category with items from db");
            }
        }

        public EditCategoryVM EditVMMapper(CategoryVM categoryVM)
        {
            return new EditCategoryVM()
            {
                Id = categoryVM.Id,
                Title = categoryVM.Title,
            };
        }
    }
}
