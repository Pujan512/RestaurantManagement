using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.ViewModels.Bill;
using Microsoft.AspNetCore.Authorization;
using RestaurantManagement.Core.Constants;

namespace RestaurantManagement.PL.Controllers
{
    [Authorize(Roles = RoleConstants.Manager + "," +RoleConstants.Cashier)]
    public class BillController(IBillServices billServices, ILogger<BillController> logger, ISettingServices settingServices) : Controller
    {
        private readonly IBillServices _billServices = billServices;
        private readonly ISettingServices _settingServices = settingServices;
        private readonly ILogger<BillController> _logger = logger;

        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Fetching all bills");
                var bills = await _billServices.GetBillsAsync();
                return View(bills);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching bill {billId}", id);
                var bill = await _billServices.GetBillAsync(id);
                if (bill == null)
                {
                    return NotFound();
                }

                return View(bill);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Bill/Create
        public async Task<IActionResult> Create(int id)
        {
            try
            {
                _logger.LogInformation("Creating billVM for table {tableId}", id);
                var billVM = await _billServices.CreateBillAsync(id);

                return View(billVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message); 
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Bill/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("PaymentType,SubTotal,TotalAmount,Discount,ServiceCharge,VAT,Timestamp")] CreateBillVM createBillVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Creating bill for table {tableId}", id);
                    int billId = await _billServices.AddBillAsync(id, createBillVM);
                    if(billId > 0) return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    ModelState.AddModelError("", ex.Message);
                }

            }
            return NoContent();
        }
    }
}
