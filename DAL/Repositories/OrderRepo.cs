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
    public class OrderRepo(RestaurantDBContext context) : IOrderRepo
    {
        private readonly RestaurantDBContext _context = context;
        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        /*
        public async Task DeleteOrderAsync(int id)
        {
            var order = await GetOrderAsync(id);
            if (order == null) throw new Exception("Order not found");

            _context.Remove(order);
            await _context.SaveChangesAsync();
        }
        public async Task EditOrderAsync()
        {
            await _context.SaveChangesAsync();
        }

        */
        public async Task<Order?> GetOrderAsync(int id)
        {
            return await _context.Order.Include(o => o.User).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Order
                .Include(o => o.Tablespace)
                .Include(o => o.User )
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithItemsAsync(int id)
        {
            return await _context.Order
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
