using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.ViewModels.OrderItem;
using RestaurantManagement.BLL.Services;
using RestaurantManagement.Core.Constants;

namespace RestaurantManagement.PL.Controllers
{
    [Authorize(Roles = RoleConstants.Waiter)]
    public class OrderItemController(ICategoryServices categoryServices, IOrderItemServices orderItemServices, IMenuItemServices menuItemServices, ILogger<OrderItemController> logger) : Controller
    {
        private readonly IOrderItemServices _orderItemServices = orderItemServices;
        private readonly IMenuItemServices _menuItemServices = menuItemServices;
        private readonly ICategoryServices _categoryServices = categoryServices;
        private readonly ILogger<OrderItemController> _logger = logger;

        // GET: OrderItem
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Fetching all order items");
                var orderItems = await _orderItemServices.GetOrderItemsAsync();
                return View(orderItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // GET: OrderItem/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            try
            {
                _logger.LogInformation("Fetching order item {orderItemId}", id);
                var orderItem = await _orderItemServices.GetOrderItemAsync(id);

                if (orderItem == null)
                {
                    return NotFound();
                }

                return View(orderItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // GET: OrderItem/Create
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            try
            {
                ViewBag.MenuItems = await _menuItemServices.GetItemsAsync();
                ViewBag.Categories = await _categoryServices.GetCategoriesAsync();
                ViewBag.OrderId = id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View();
        }

        // POST: OrderItem/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("MenuItemId,Quantity")] AddOrderItemVM addOrderItemVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Creating order item");
                    bool success = await _orderItemServices.CreateOrderItemAsync(id, addOrderItemVM);
                    if(success) return RedirectToAction("Details", "Order", new { id });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            try
            {
                ViewBag.MenuItemId = new SelectList(await _menuItemServices.GetItemsAsync(), "Id", "Name");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View(addOrderItemVM);
        }

        // GET: OrderItem/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching order item {orderItemId}", id);
                var orderItem = await _orderItemServices.GetOrderItemAsync(id);
                if (orderItem == null)
                {
                    return NotFound();
                }
                ViewBag.MenuItemId = new SelectList(await _menuItemServices.GetItemsAsync(), "Id", "Name");
                return View(_orderItemServices.EditVMMapper(orderItem));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // POST: OrderItem/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MenuItemId,Quantity,OrderId")] EditOrderItemVM editOrderItemVM)
        {
            if (id != editOrderItemVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Editing order item {orderItemId}", id);
                    bool success = await _orderItemServices.EditOrderItemAsync(id, editOrderItemVM);
                    if(success) return RedirectToAction("Details", "Order", new { id = editOrderItemVM.OrderId });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                
            }
            try
            {
                ViewBag.MenuItemId = new SelectList(await _menuItemServices.GetItemsAsync(), "Id", "Name");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View(editOrderItemVM);
        }

        // GET: OrderItem/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching order item {orderItemId}", id);
                var orderItem = await _orderItemServices.GetOrderItemAsync(id);
                if (orderItem == null)
                {
                    return NotFound();
                }

                return View(orderItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // POST: OrderItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _logger.LogInformation("Fetching order item {orderItemId}", id);
                var orderItem = await _orderItemServices.GetOrderItemAsync(id);
                if (orderItem == null) return NotFound();
                _logger.LogInformation("Deleting order item {orderItemId}", id);
                bool success = await _orderItemServices.DeleteOrderItemAsync(id);
                if(success) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

    }
}
