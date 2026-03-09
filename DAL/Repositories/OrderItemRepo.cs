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
    public class OrderItemRepo(RestaurantDBContext context) : IOrderItemRepo
    {
        private readonly RestaurantDBContext _context = context;
        /*
        public async Task CreateOrderItemAsync(OrderItem orderItem)
        {
            await _context.OrderItem.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await GetOrderItemAsync(id);
            if (orderItem == null) throw new Exception("Unable to delete orderItem from db");

            _context.OrderItem.Remove(orderItem);
            await _context.SaveChangesAsync();
        }*/

        public async Task<OrderItem?> GetOrderItemAsync(int id)
        {
            return await _context.OrderItem.Include(oi => oi.MenuItem)
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync()
        {
            return await _context.OrderItem.AsNoTracking()
                .Include(oi => oi.MenuItem)
                .ToListAsync();
        }

    }
}
