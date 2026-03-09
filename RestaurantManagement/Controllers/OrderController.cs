using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Constants;
using RestaurantManagement.Core.Utilities;
using RestaurantManagement.Core.ViewModels.Order;

namespace RestaurantManagement.PL.Controllers
{
    [Authorize(Roles = RoleConstants.Cook + "," + RoleConstants.Waiter)]
    public class OrderController(IOrderServices orderServices, Utils utils, ILogger<OrderController> logger) : Controller
    {
        private readonly IOrderServices _orderServices = orderServices;
        private readonly Utils _utils = utils;
        private readonly ILogger<OrderController> _logger = logger;

        // GET: Order
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Fetching all orders");
                var orders = await _orderServices.GetOrdersAsync();
                List<string> priorityOrders = ["Urgent", "Medium"];
                List<string> statusOrders = ["Pending", "Preparing", "Ready", "Cancelled"];
                var sortedOrders = orders
                    .OrderBy(o => priorityOrders.IndexOf(o.Priority))
                    .ThenBy(o => statusOrders.IndexOf(o.Status))
                    .ThenBy(o => o.OrderTime)
                    .ToList();
                return View(sortedOrders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching order {orderId}", id);
                var order = await _orderServices.GetOrderWithItemsAsync(id);
                if (order == null)
                {
                    return NotFound();
                }
                return View(order);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        //GET: Order/Create
        [Authorize(Roles = RoleConstants.Waiter)]
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Order/Create
        [Authorize(Roles = RoleConstants.Waiter)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("OrderTime,IsModified,Status,Priority,UserId")] AddOrderVM addOrderVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Creating order for tablespace {tablespaceId}", id);
                    var order = await _orderServices.AddOrderAsync(id, addOrderVM);
                    if (order != null) return RedirectToAction("Details", "Order", new { id = order.Id });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(addOrderVM);
        }

        // POST: Order/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderTime,IsModified,Status,Priority,TablespaceId")] EditOrderVM editOrderVM)
        {
            if (id != editOrderVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Editing order {orderId}", id);
                    bool success = await _orderServices.EditOrderAsync(id, editOrderVM);
                    if (success)
                    {
                        var user = await _utils.GetActiveUser();
                        if (user!.Role == "Cook")
                        {
                            return RedirectToAction("Index", "Order");
                        }
                        return RedirectToAction("Details", "Tablespace", new { id = editOrderVM.TablespaceId });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(editOrderVM);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching order {orderId}", id);
                var order = await _orderServices.GetOrderAsync(id);
                if (order == null)
                {
                    return NotFound();
                }

                return View(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _logger.LogInformation("Deleting order {orderId}", id);
                bool success = await _orderServices.DeleteOrderAsync(id);
                if (success) return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }
    }
}
