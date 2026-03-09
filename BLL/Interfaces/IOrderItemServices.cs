using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.OrderItem;

namespace RestaurantManagement.BLL.Interfaces
{
    public interface IOrderItemServices
    {
        public Task<IEnumerable<OrderItemVM>> GetOrderItemsAsync();
        public Task<OrderItemVM?> GetOrderItemAsync(int id);
        public Task<bool> CreateOrderItemAsync(int orderId, AddOrderItemVM orderItemVM);
        public Task<bool> EditOrderItemAsync(int id, EditOrderItemVM orderItemVM);
        public Task<bool> DeleteOrderItemAsync(int id);
        public EditOrderItemVM EditVMMapper(OrderItemVM orderItem);

    }
}
