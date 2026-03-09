using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Constants;
using RestaurantManagement.Core.ViewModels.Category;

namespace RestaurantManagement.PL.Controllers
{
    [Authorize(Roles = RoleConstants.Manager)]
    public class CategoryController(ICategoryServices categoryServices, ILogger<CategoryController> logger) : Controller
    {

        private readonly ICategoryServices _categoryServices = categoryServices;
        private readonly ILogger<CategoryController> _logger = logger;
        // GET: Category
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Fetching all categories");
                var categories = await _categoryServices.GetCategoriesAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                _logger.LogInformation("Fetching details for category {categoryId}", id);
                var category = await _categoryServices.GetCategoryWithItemsAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return NoContent();
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title")] AddCategoryVM addCategoryVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Creating new category");
                    bool success = await _categoryServices.AddCategoryAsync(addCategoryVM);
                    if (success) return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("", ex.Message);
            }
            return View(addCategoryVM);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            try
            {

                _logger.LogInformation("Fetching category {categoryId}", id);
                var category = await _categoryServices.GetCategoryAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                _logger.LogInformation("Editing category {categoryId}", id);
                return View(_categoryServices.EditVMMapper(category));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] EditCategoryVM editCategoryVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Updating category {categoryId}", id);
                    bool success = await _categoryServices.EditCategoryAsync(id, editCategoryVM);
                    if (success) return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }

            }
            return View(editCategoryVM);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching category {categoryId}", id);
                var category = await _categoryServices.GetCategoryAsync(id);
                return View(category);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction(nameof(Details));
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _logger.LogInformation("Deleting category {categoryId}", id);
                bool success = await _categoryServices.DeleteCategoryAsync(id);
                if (success) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

    }
}
