using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;

namespace RestaurantManagement.DAL.Interfaces
{
    public interface IOrderItemRepo
    {
        public Task<IEnumerable<OrderItem>> GetOrderItemsAsync();
        public Task<OrderItem?> GetOrderItemAsync(int id);
        /*
        public Task CreateOrderItemAsync(OrderItem orderItem);
        public Task UpdateOrderItemAsync();
        public Task DeleteOrderItemAsync(int id);
        */
    }
}
