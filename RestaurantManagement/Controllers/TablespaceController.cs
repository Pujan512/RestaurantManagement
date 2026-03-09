using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.ViewModels.Tablespace;
using Serilog;
using RestaurantManagement.Core.Constants;

namespace RestaurantManagement.PL.Controllers
{
    [Authorize(Roles = RoleConstants.Manager + "," + RoleConstants.Waiter + "," + RoleConstants.Cashier)]
    public class TablespaceController(ITableServices tableServices, ILogger<TablespaceController> logger) : Controller
    {
        private readonly ITableServices _tableServices = tableServices;
        private readonly ILogger<TablespaceController> _logger = logger;

        // GET: Tablespace
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Fetching all tables");
                var tables = await _tableServices.GetTablesAsync();
                return View(tables);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // GET: Tablespace/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching details for table {tableId}", id);
                var tablespace = await _tableServices.GetTableWithMenuItemsAsync(id);
                if (tablespace == null)
                {
                    return NotFound();
                }

                return View(tablespace);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return RedirectToAction("Index");
        }

        // GET: Tablespace/Create
        [Authorize(Roles = RoleConstants.Manager)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tablespace/Create
        [Authorize(Roles = RoleConstants.Manager)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TableNumber,IsOccupied,Capacity")] AddTableVM addTableVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Creating new table");
                    bool success = await _tableServices.AddTableAsync(addTableVM);
                    if (success) return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return View(addTableVM);
        }

        // GET: Tablespace/Edit/5
        [Authorize(Roles = RoleConstants.Manager + ", " + RoleConstants.Waiter)]
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching table {tableId}", id);
                var tablespace = await _tableServices.GetTableAsync(id);
                if (tablespace == null)
                {
                    return NotFound();
                }
                return View(_tableServices.EditVMMapper(tablespace));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return NoContent();
        }

        // POST: Tablespace/Edit/5
        [Authorize(Roles = RoleConstants.Manager + ", " + RoleConstants.Waiter)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsOccupied,Capacity")] EditTableVM editTableVM)
        {
            if (id != editTableVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Updating table {tableId}", id);
                    bool success = await _tableServices.UpdateTableAsync(id, editTableVM);
                    if (success) return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(editTableVM);
        }

        // GET: Tablespace/Delete/5
        [Authorize(Roles = RoleConstants.Manager)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching table {tableId}", id);
                var tablespace = await _tableServices.GetTableAsync(id);
                if (tablespace == null)
                {
                    return NotFound();
                }

                return View(tablespace);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // POST: Tablespace/Delete/5
        [Authorize(Roles = RoleConstants.Manager)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _logger.LogInformation("Fetching table {tableId}", id);
                var tablespace = await _tableServices.GetTableAsync(id);
                _logger.LogInformation("Deleting table {tableId}", id);
                bool success = await _tableServices.DeleteTableAsync(id);
                if (success) return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
            return NoContent();
        }
    }
}
