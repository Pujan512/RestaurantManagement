using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Models;
using RestaurantManagement.DAL.Data;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.DAL.Repositories
{
    public class MenuItemRepo(RestaurantDBContext context) : IMenuItemRepo
    {
        private readonly RestaurantDBContext _context = context;
        /*
        public async Task AddItemAsync(MenuItem menuItem)
        {
            await _context.MenuItem.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(int id)
        {
            var menuItem = await GetItemAsync(id);
            if (menuItem == null) throw new Exception("Unable to delete item from database");

            _context.Remove(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task EditItemAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<MenuItem?> GetItemAsync(int id)
        {
            return await _context.MenuItem.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<MenuItem>> GetItemsAsync()
        {
            return await _context.MenuItem
                .AsNoTracking()
                .ToListAsync();
        }*/
    }
}
