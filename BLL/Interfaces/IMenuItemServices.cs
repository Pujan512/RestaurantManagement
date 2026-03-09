using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.MenuItem;

namespace RestaurantManagement.BLL.Interfaces
{
    public interface IMenuItemServices
    {
        public Task<IEnumerable<MenuItemVM>> GetItemsAsync();
        public Task<MenuItemVM?> GetItemAsync(int id);
        public Task<bool> AddItemAsync(int categoryId, AddItemVM itemVM, IFormFile? file);
        public Task<bool> DeleteItemAsync(int id);
        public Task<bool> EditItemAsync(int id, EditItemVM itemVM, IFormFile? file);
        public EditItemVM EditVMMapper(MenuItemVM item);
    }
}
