using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;

namespace RestaurantManagement.DAL.Interfaces
{
    public interface IOrderRepo
    {
        public Task<IEnumerable<Order>> GetOrdersAsync();
        public Task<Order?> GetOrderAsync(int id);
        public Task<Order?> GetOrderWithItemsAsync(int id);
        public Task<Order> AddOrderAsync(Order order);
        /*
        public Task EditOrderAsync();
        public Task DeleteOrderAsync(int id);
        */
    }
}
