using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Order;

namespace RestaurantManagement.BLL.Interfaces
{
    public interface IOrderServices
    {
        public Task<IEnumerable<OrderVM>> GetOrdersAsync();
        public Task<OrderVM?> GetOrderAsync(int id);
        public Task<OrderVM?> GetOrderWithItemsAsync(int id);
        public Task<OrderVM?> AddOrderAsync(int tablespaceId, AddOrderVM orderVM);
        public Task<bool> DeleteOrderAsync(int id);
        public Task<bool> EditOrderAsync(int id, EditOrderVM orderVM);
        public EditOrderVM EditVMMapper(OrderVM orderVM);
    }
}
