using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.OrderItem;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.BLL.Services
{
    public class OrderItemServices(IOrderItemRepo orderItemRepo, IOrderRepo orderRepo, IMapper mapper, IGenericRepository<OrderItem> genericOrderItemRepo) : IOrderItemServices
    {
        private readonly IOrderItemRepo _orderItemRepo = orderItemRepo;
        private readonly IGenericRepository<OrderItem> _genericOrderItemRepo = genericOrderItemRepo;
        private readonly IOrderRepo _orderRepo = orderRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<bool> CreateOrderItemAsync(int orderId, AddOrderItemVM addOrderItemVM)
        {
            OrderItem orderItem = new()
            {
                MenuItemId = addOrderItemVM.MenuItemId,
                Quantity = addOrderItemVM.Quantity,
                OrderId = orderId
            };
            try
            {
                await _genericOrderItemRepo.AddAsync(orderItem);
                return true;
            }
            catch
            {
                throw new Exception("Unable to create order");
            }
        }

        public async Task<bool> DeleteOrderItemAsync(int id)
        {
            var orderItem = await _orderItemRepo.GetOrderItemAsync(id);
            if (orderItem == null) throw new Exception("OrderItem not found");

            try
            {
                await _genericOrderItemRepo.DeleteAsync(id);
                return true;
            }
            catch
            {
                throw new Exception("Unable to delete order from db");
            }
        }

        public async Task<bool> EditOrderItemAsync(int id, EditOrderItemVM editOrderItemVM)
        {
            var orderItem = await _orderItemRepo.GetOrderItemAsync(id);
            if (orderItem == null) throw new Exception("OrderItem not found");
            var order = await _orderRepo.GetOrderAsync(orderItem.OrderId);
            order.IsModified = true;

            orderItem.MenuItemId = editOrderItemVM.MenuItemId;
            orderItem.Quantity = editOrderItemVM.Quantity;

            try
            {
                await _genericOrderItemRepo.EditAsync();
                return true;
            }
            catch
            {
                throw new Exception("Unable to update order in db");
            }
        }

        public EditOrderItemVM EditVMMapper(OrderItemVM orderItemVM)
        {
            return new EditOrderItemVM()
            {
                Id = orderItemVM.Id,
                Quantity = orderItemVM.Quantity,
                MenuItemId = orderItemVM.MenuItemId,
                OrderId = orderItemVM.OrderId
            };
        }

        public async Task<OrderItemVM?> GetOrderItemAsync(int id)
        {
            try
            {
                var orderItem = await _orderItemRepo.GetOrderItemAsync(id);
                return _mapper.Map<OrderItemVM>(orderItem);
            }
            catch
            {
                throw new Exception("Unable to get order item from db");
            }
        }

        public async Task<IEnumerable<OrderItemVM>> GetOrderItemsAsync()
        {
            try
            {
                var orderItems = await _orderItemRepo.GetOrderItemsAsync();
                return _mapper.Map<IEnumerable<OrderItemVM>>(orderItems);
            }
            catch
            {
                throw new Exception("Unable to get order items from db");
            }
        }
    }
}
