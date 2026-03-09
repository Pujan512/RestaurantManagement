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
using RestaurantManagement.Core.ViewModels.MenuItem;

namespace RestaurantManagement.PL.Controllers
{
    [Authorize(Roles = RoleConstants.Manager)]
    public class MenuItemController(IMenuItemServices menuItemServices, ICategoryServices categoryServices, ILogger<MenuItemController> logger) : Controller
    {
        private readonly ICategoryServices _categoryServices = categoryServices;
        private readonly IMenuItemServices _menuItemServices = menuItemServices;
        private readonly ILogger<MenuItemController> _logger = logger;

        public async Task<IActionResult> Create(int id)
        {
            ViewBag.CategoryId = id;
            var category = await _categoryServices.GetCategoryAsync(id);
            ViewBag.CategoryTitle = category!.Title;
            return View();
        }

        // POST: MenuItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Name,Price,Discount,Description,IsAvailable")] AddItemVM addItemVM, IFormFile? ImageUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Creating menu item");
                    bool success = await _menuItemServices.AddItemAsync(id, addItemVM, ImageUrl);
                    if(success) return RedirectToAction("Details", "Category", new { id });
                }
                return View(addItemVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("", ex.Message);
            }
            return NoContent();
        }

        // GET: MenuItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching menu item {menuItemId}", id);
                var menuItem = await _menuItemServices.GetItemAsync(id);
                if (menuItem == null)
                {
                    return NotFound();
                }
                ViewBag.CategoryId = menuItem.CategoryId;
                var category = await _categoryServices.GetCategoryAsync(menuItem.CategoryId);
                ViewBag.CategoryTitle = category.Title;
                _logger.LogInformation("Editing menu item {menuItemId}", id);
                var editVM = _menuItemServices.EditVMMapper(menuItem);
                return View(editVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("", ex.Message);
            }
            return NoContent();
        }

        // POST: MenuItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Discount,Description,IsAvailable, CategoryId")] EditItemVM editItemVM, IFormFile? ImageUrl)
        {
            if (id != editItemVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Updating menu item {menuItemId}", id);
                    bool success = await _menuItemServices.EditItemAsync(id, editItemVM, ImageUrl);
                    if(success) return RedirectToAction("Details", "Category", new { id = editItemVM.CategoryId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    ModelState.AddModelError("", ex.Message);
                }
                
            }
            return View(editItemVM);
        }

        // GET: MenuItem/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching menu item {menuItemId}", id);
                var menuItem = await _menuItemServices.GetItemAsync(id);
                if (menuItem == null)
                {
                    return NotFound();
                }
                var category = await _categoryServices.GetCategoryAsync(menuItem.CategoryId);
                ViewBag.CategoryTitle = category!.Title;

                return View(menuItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("", ex.Message);
            }
            return NoContent();
        }

        // POST: MenuItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _logger.LogInformation("Fetching menu item {menuItemId}", id);
                var menuItem = await _menuItemServices.GetItemAsync(id);
                if (menuItem == null)
                {
                    return NotFound();
                }

                _logger.LogInformation("Deleting menu item {menuItemId}", id);
                bool success = await _menuItemServices.DeleteItemAsync(id);
                if(success) return RedirectToAction("Details", "Category", new { id = menuItem.CategoryId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("", ex.Message);
            }
            return NoContent();
        }
    }
}
