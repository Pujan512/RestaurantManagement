using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Bill;
using RestaurantManagement.Core.ViewModels.Order;
using RestaurantManagement.Core.ViewModels.Tablespace;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.BLL.Services
{
    public class BillServices(IBillRepo billRepo,
        IOrderRepo orderRepo,
        ITableServices tableServices, 
        ISettingServices settingServices,
        IMapper mapper) : IBillServices
    {
        private readonly IBillRepo _billRepo = billRepo;
        private readonly IOrderRepo _orderRepo = orderRepo;
        private readonly IMapper _mapper = mapper;
        private readonly ITableServices _tableServices = tableServices;
        private readonly ISettingServices _settingServices = settingServices;
        public async Task<int> AddBillAsync(int tablespaceId, CreateBillVM createBillVM)
        {
            var newBillVM = await CreateBillAsync(tablespaceId);
            var existingOrders = new List<Order>();
            foreach (var orderVM in newBillVM.OrderVMs)
            {
                var existingOrder = await _orderRepo.GetOrderAsync(orderVM.Id);
                if (existingOrder == null)
                {
                    throw new Exception($"Order with ID {orderVM.Id} not found");
                }
                existingOrders.Add(existingOrder);
            }
            Bill newBill = new()
            {
                PaymentType = createBillVM.PaymentType,
                Timestamp = newBillVM.Timestamp,
                SubTotal = newBillVM.SubTotal,
                VAT = newBillVM.VAT,
                VATPercent = newBillVM.VATPercent,
                ServiceCharge = newBillVM.ServiceCharge,
                ServiceChargePercent = newBillVM.ServiceChargePercent,
                TotalAmount = newBillVM.TotalAmount,
                TablespaceId = tablespaceId,
                Orders = existingOrders
            };
            try
            {
                await _billRepo.GenerateBillAsync(newBill);
                return newBill.Id;
            }
            catch
            {
                throw new Exception("Unable to generate bill");
            }

        }

        public async Task<BillVM?> GetBillAsync(int id)
        {
            try
            {
                var bill = await _billRepo.GetBillAsync(id);
                return _mapper.Map<BillVM>(bill);
            }
            catch
            {
                throw new Exception("Unable to get bill from db");
            }
        }

        public async Task<IEnumerable<BillVM>> GetBillsAsync()
        {
            try
            {
                var bills = await _billRepo.GetAllBillsAsync();
                return _mapper.Map<IEnumerable<BillVM>>(bills);
            }
            catch
            {
                throw new Exception("Unable to get bills from db");
            }
        }

        public async Task<BillVM> CreateBillAsync(int tablespaceId)
        {
            var tablespace = await _tableServices.GetTableWithMenuItemsAsync(tablespaceId);
            if (tablespace == null) throw new Exception("Tablespace not found");

            var systemSetting = await _settingServices.GetSystemSettingAsync();
            if (systemSetting == null) throw new Exception("System setting not found");

            decimal subTotal = 0.00m;
            List<OrderVM> orderVMs = [];
            foreach (var order in tablespace!.OrderVMs)
            {
                if (order.Status == "Ready")
                {
                    foreach (var orderItem in order.OrderItemVMs)
                    {
                        subTotal += (orderItem.MenuItemVM.Price - (orderItem.MenuItemVM.Discount * orderItem.MenuItemVM.Price / 100)) * orderItem.Quantity;
                    }
                    orderVMs.Add(order);
                }

                else if (order.Status == "Paid") continue;
                else throw new Exception("Order isn't ready yet");
            }

            if (subTotal == 0) throw new Exception("No order items were found");
            decimal VAT = systemSetting.VAT * subTotal / 100;
            decimal serviceCharge = systemSetting.ServiceCharge * subTotal / 100;
            decimal totalAmount = subTotal + VAT + serviceCharge;

            return new BillVM()
            {
                TablespaceId = tablespaceId,
                Tablespace = tablespace,
                Timestamp = DateTime.Now,
                SubTotal = subTotal,
                VAT = VAT,
                ServiceCharge = serviceCharge,
                TotalAmount = totalAmount,
                OrderVMs = orderVMs,

                VATPercent = systemSetting.VAT,
                ServiceChargePercent = systemSetting.ServiceCharge
            };
        }
    }
}
