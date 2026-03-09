using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.BLL.Services;
using RestaurantManagement.Core.Utilities;

namespace RestaurantManagement.PL.Controllers
{
    public class HomeController(ILogger<HomeController> logger, Utils utils, IScheduleServices scheduleServices, IBillServices billServices, IOrderServices orderServices, ITableServices tableServices) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly Utils _utils = utils;
        private readonly IScheduleServices _scheduleServices = scheduleServices;
        private readonly IBillServices _billServices = billServices;
        private readonly IOrderServices _orderServices = orderServices;
        private readonly ITableServices _tableServices = tableServices;

        public async Task<IActionResult> Index()
        {
            try
            {

                var user = await _utils.GetActiveUser();
                if (user != null)
                {
                    ViewBag.UserName = user.FirstName + " " + user.MiddleName + " " + user.LastName;
                    ViewBag.Role = user.Role;
                    ViewBag.Email = user.Email;
                    if (user.Role != "Admin")
                    {
                        var schedule = await _scheduleServices.GetScheduleVMByUserIdAsync(user.Id);
                        ViewBag.ShiftStart = schedule.ShiftStart;
                        ViewBag.ShiftEnd = schedule.ShiftEnd;
                    } else
                    {
                        var bills = await _billServices.GetBillsAsync();
                        var orders = await _orderServices.GetOrdersAsync();
                        var tables = await _tableServices.GetTablesAsync();

                        ViewBag.OrdersCount = orders.Where(o => o.OrderTime.Date == DateTime.Now.Date).Count();
                        ViewBag.PendingOrdersCount = orders.Where(o => o.OrderTime.Date == DateTime.Now.Date && (o.Status != "Ready" && o.Status != "Paid")).Count();
                        ViewBag.Earnings = bills.Where(o => o.Timestamp.Date == DateTime.Now.Date)
                                                    .Sum(o => o.TotalAmount);
                        ViewBag.AvailableTablesCount = tables.Where(t => !t.IsOccupied).Count();

                        var dailyEarnings = bills
                            .GroupBy(b => b.Timestamp.Date)
                            .Take(7)
                            .Select(d => new
                            {
                                Date = d.Key.ToString("M/dd"),
                                Amount = d.Sum(b => b.TotalAmount)
                            })
                            .OrderBy(d => d.Date)
                            .ToList();

                        ViewBag.data = dailyEarnings;
                    }
                } else
                {
                    return RedirectToAction("Login", "Account");
                }
                    return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error");
            }
        }

        public IActionResult Error()
        {
            ViewData["Message"] = TempData.Peek("ErrorMessage");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
