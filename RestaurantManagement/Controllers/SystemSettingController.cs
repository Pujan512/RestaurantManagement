using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Models;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.ViewModels.SystemSetting;
using Microsoft.AspNetCore.Authorization;
using RestaurantManagement.Core.Constants;

namespace RestaurantManagement.PL.Controllers
{
    [Authorize(Roles = RoleConstants.Manager)]
    public class SystemSettingController(ISettingServices settingServices, ILogger<SystemSettingController> logger) : Controller
    {
        private readonly ISettingServices _settingServices = settingServices;
        private readonly ILogger<SystemSettingController> _logger = logger;

        // GET: SystemSetting
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Fetching system setting");
                var systemSettings = await _settingServices.GetSystemSettingAsync();
                return View(systemSettings);

            }
            catch (Exception ex)
            {
                {
                    _logger.LogError(ex.Message);
                }
                return NoContent();
            }
        }


        // GET: SystemSetting/Edit/5
        public async Task<IActionResult> Edit()
        {
            try
            {
                _logger.LogInformation("Fetching system setting");
                var systemSettingVM = await _settingServices.GetSystemSettingAsync();
                if (systemSettingVM == null)
                {
                    return NotFound();
                }
                return View(_settingServices.EditVMMapper(systemSettingVM));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // POST: SystemSetting/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("VAT,Discount,ServiceCharge")] EditSettingVM editSettingVM)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Updating system setting");
                    var updatedSettingVM = await _settingServices.EditSettingAsync(editSettingVM);
                    if (updatedSettingVM == null) return NotFound();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return View(editSettingVM);
        }


    }
}
