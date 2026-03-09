using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.BLL.Services;
using RestaurantManagement.Core.Constants;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Schedule;
using RestaurantManagement.DAL.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantManagement.PL.Controllers
{
    [Authorize(Roles = RoleConstants.Manager)]
    public class ScheduleController(IScheduleServices scheduleServices, IMapper mapper, ILogger<ScheduleController> logger, IAccountServices accountServices) : Controller
    {
        private readonly IScheduleServices _scheduleServices = scheduleServices;
        private readonly IAccountServices _accountServices = accountServices;
        private readonly ILogger<ScheduleController> _logger = logger;
        private readonly IMapper _mapper = mapper;

        // GET: Schedule
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Fetching all schedules");
                var schedules = await _scheduleServices.GetAllSchedulesAsync();
                return View(schedules);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // GET: Schedule/Create
        public async Task<IActionResult> Create()
        {
            var users = (await _accountServices.GetAllUsers())
                        .Select(u => new { u.Id, FullName = $"{u.FirstName} {u.LastName}" });

            ViewBag.UserId = new SelectList(users, "Id", "FullName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ShiftStart,ShiftEnd")] AddScheduleVM addScheduleVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Creating new schedule");
                    bool success = await _scheduleServices.AddScheduleAsync(addScheduleVM);
                    if (success) return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    _logger.LogError(ex.Message);
                }
            }
            var users = (await _accountServices.GetAllUsers())
                        .Select(u => new { u.Id, FullName = $"{u.FirstName} {u.LastName}" });

            ViewBag.UserId = new SelectList(users, "Id", "FullName");
            return View(addScheduleVM);
        }

        // GET: Schedule/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching schedule {id} for editing", id);
                var schedule = await _scheduleServices.GetScheduleVMAsync(id); 
                var users = (await _accountServices.GetAllUsers())
                        .Select(u => new { u.Id, FullName = $"{u.FirstName} {u.LastName}" });

                ViewBag.UserId = new SelectList(users, "Id", "FullName");
                if (schedule == null)
                {
                    return NotFound();
                }
                return View(_mapper.Map<EditScheduleVM>(schedule));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShiftStart,ShiftEnd")] EditScheduleVM editScheduleVM)
        {
            if (id != editScheduleVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Editing schedule {id}", id);
                    bool success = await _scheduleServices.EditScheduleAsync(id, editScheduleVM);
                    if (success) return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    _logger.LogError(ex.Message);
                }
            }
            return View(editScheduleVM);
        }

        // GET: Schedule/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            try
            {
                _logger.LogInformation("Fetching schedule {id} for deletion", id);
                var schedule = await _scheduleServices.GetScheduleVMAsync(id);
                if (schedule == null)
                {
                    return NotFound();
                }

                return View(schedule);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return NoContent();
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                _logger.LogInformation("Deleting schedule {id}", id);
                bool success = await _scheduleServices.RemoveScheduleAsync(id);
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
