using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.MenuItem;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.BLL.Services
{
    public class MenuItemServices(IGenericRepository<MenuItem> genericMenuItemRepo, IMapper mapper) : IMenuItemServices
    {
        private readonly IGenericRepository<MenuItem> _genericMenuItemRepo = genericMenuItemRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<bool> AddItemAsync(int categoryId, AddItemVM addItemVM, IFormFile? file)
        {
            string filePath = "";
            if (file != null && file.Length > 0)
                filePath = await UploadAndGetImageAsync(file);

            MenuItem item = new()
            {
                Name = addItemVM.Name,
                Price = addItemVM.Price,
                Description = addItemVM.Description,
                ImageUrl = filePath,
                IsAvailable = addItemVM.IsAvailable,
                CategoryId = categoryId,
                Discount = addItemVM.Discount
            };

            try
            {
                await _genericMenuItemRepo.AddAsync(item);
                return true;
            }
            catch
            {
                throw new Exception("Unable to add menu item in db");
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var item = await _genericMenuItemRepo.GetByIdAsync(id);
            if (item == null) throw new Exception("Item not found");

            try
            {
                await _genericMenuItemRepo.DeleteAsync(id);
                return true;
            }
            catch
            {
                throw new Exception("Unable to delete menu item from db");
            }
        }

        public async Task<bool> EditItemAsync(int id, EditItemVM editItemVM, IFormFile? file)
        {
            string filePath = "";
            if (file != null && file.Length > 0)
                filePath = await UploadAndGetImageAsync(file);
            var item = await _genericMenuItemRepo.GetByIdAsync(id);
            if (item == null) throw new Exception("Item not found");
            if (id != editItemVM.Id) throw new Exception("Item id doesn't match");

            item.Name = editItemVM.Name;
            item.Price = editItemVM.Price;
            item.Description = editItemVM.Description;
            item.CategoryId = editItemVM.CategoryId;
            item.ImageUrl = filePath == "" ? item.ImageUrl : filePath;
            item.IsAvailable = editItemVM.IsAvailable;
            item.Discount = editItemVM.Discount;

            try
            {
                await _genericMenuItemRepo.EditAsync();
                return true;
            }
            catch
            {
                throw new Exception("Unable to update menu item in db");
            }

        }

        public async Task<MenuItemVM?> GetItemAsync(int id)
        {
            try
            {
                var menuItem = await _genericMenuItemRepo.GetByIdAsync(id);
                return _mapper.Map<MenuItemVM>(menuItem);
            } catch
            {
                throw new Exception("Unable to get menu item from db");
            }
        }

        public async Task<IEnumerable<MenuItemVM>> GetItemsAsync()
        {
            try
            {
                var menuItems = await _genericMenuItemRepo.GetAllAsync();
                return _mapper.Map<IEnumerable<MenuItemVM>>(menuItems);
            }
            catch
            {
                throw new Exception("Unable to get menu items from db");
            }
        }


        public EditItemVM EditVMMapper(MenuItemVM itemVM)
        {
            return new EditItemVM()
            {
                Id = itemVM.Id,
                Name = itemVM.Name,
                Description = itemVM.Description,
                CategoryId = itemVM.CategoryId,
                ImageUrl = itemVM.ImageUrl
            };
        }

        public async Task<string> UploadAndGetImageAsync(IFormFile file)
        {
            string filePath = "";
            Guid guid = Guid.NewGuid();
            var allowedExtensions = new[] { ".jpg", ".png" };
            var extension = Path.GetExtension(file.FileName);

            if (!allowedExtensions.Contains(extension.ToLower()))
            {
                throw new ArgumentException("Invalid file format!");
            }

            string fileName = guid + extension;
            filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            filePath = "/Images/" + fileName;
            return filePath;

        }

    }
}
