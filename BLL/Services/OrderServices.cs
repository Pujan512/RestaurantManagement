using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.Utilities;
using RestaurantManagement.Core.ViewModels.Order;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.BLL.Services
{
    public class OrderServices(IOrderRepo orderRepo, Utils utils, IMapper mapper, IGenericRepository<Order> genericOrderRepo) : IOrderServices
    {
        private readonly IOrderRepo _orderRepo = orderRepo;
        private readonly IGenericRepository<Order> _genericOrderRepo = genericOrderRepo;
        private readonly IMapper _mapper = mapper;
        private readonly Utils _utils = utils;
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _orderRepo.GetOrderAsync(id);
            if (order == null) throw new Exception("Order not found");

            try
            {
                await _genericOrderRepo.DeleteAsync(id);
                return true;
            }
            catch
            {
                throw new Exception("Unable to delete order");
            }

        }

        public async Task<bool> EditOrderAsync(int id, EditOrderVM editOrderVM)
        {
            var order = await _orderRepo.GetOrderAsync(id);
            var currentUser = await _utils.GetActiveUser();
            if (order == null) throw new Exception("Order not found");
            if (id != editOrderVM.Id) throw new Exception("Order id doesn't match");
            if (currentUser!.Role == "Waiter" && order.User!.Id != currentUser!.Id) throw new Exception("Unauthorized attempt to edit order");
            order.OrderTime = editOrderVM.OrderTime;
            order.Status = editOrderVM.Status == null ? order.Status : editOrderVM.Status;
            order.Priority = editOrderVM.Priority == null ? order.Priority : editOrderVM.Priority; 
            try
            {
                await _genericOrderRepo.EditAsync();
                return true;
            }
            catch
            {
                throw new Exception("Unable to update order in db");
            }
        }

        public EditOrderVM EditVMMapper(OrderVM orderVM)
        {
            return new EditOrderVM()
            {
                Id = orderVM.Id,
                Status = orderVM.Status,
                TablespaceId = orderVM.TablespaceId
            };
        }

        public async Task<OrderVM?> GetOrderAsync(int id)
        {
            try
            {
                var order = await _orderRepo.GetOrderAsync(id);
                return _mapper.Map<OrderVM>(order);
            }
            catch
            {
                throw new Exception("Unable to get order from db");
            }
        }

        public async Task<IEnumerable<OrderVM>> GetOrdersAsync()
        {
            try
            {
                var orders = await _orderRepo.GetOrdersAsync();
                return _mapper.Map<IEnumerable<OrderVM>>(orders);
            }
            catch
            {
                throw new Exception("Unable to get orders from db");
            }
        }

        public async Task<OrderVM?> GetOrderWithItemsAsync(int id)
        {
            try
            {
                var order = await _orderRepo.GetOrderWithItemsAsync(id);
                return _mapper.Map<OrderVM>(order);
            }
            catch
            {
                throw new Exception("Unable to get orders with items from db");
            }
        }

        public async Task<OrderVM?> AddOrderAsync(int tableSpaceId, AddOrderVM addOrderVM)
        {
            Order order = new()
            {
                OrderTime = addOrderVM.OrderTime,
                IsModified = addOrderVM.IsModified,
                Status = addOrderVM.Status,
                Priority = addOrderVM.Priority,
                TablespaceId = tableSpaceId,
                User = await _utils.GetActiveUser()
            };
            try
            {
                var newOrder = await _orderRepo.AddOrderAsync(order);
                return _mapper.Map<OrderVM>(newOrder);
            }
            catch
            {
                throw new ArgumentException("Unable to add Order");
            }
        }

    }
}
