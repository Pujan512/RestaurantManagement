using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels;
using RestaurantManagement.Core.ViewModels.Schedule;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.BLL.Services
{
    public class ScheduleServices(IScheduleRepo scheduleRepo, IMapper mapper, IGenericRepository<Schedule> genericScheduleRepo) : IScheduleServices
    {
        private readonly IScheduleRepo _scheduleRepo = scheduleRepo;
        private readonly IGenericRepository<Schedule> _genericScheduleRepo = genericScheduleRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<bool> AddScheduleAsync(AddScheduleVM addScheduleVM)
        {
            var schedule = await _scheduleRepo.GetScheduleByUserIdAsync(addScheduleVM.UserId);
            if (schedule != null) throw new Exception("User's schedule already exists");

            var newSchedule = new Schedule()
            {
                ShiftStart = addScheduleVM.ShiftStart,
                ShiftEnd = addScheduleVM.ShiftEnd,
                UserId = addScheduleVM.UserId
            };

            try
            {
                await _genericScheduleRepo.AddAsync(newSchedule);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EditScheduleAsync(int id, EditScheduleVM editScheduleVM)
        {
            var schedule = await _genericScheduleRepo.GetByIdAsync(id);
            if (schedule == null) throw new Exception("Schedule not found.");

            schedule.ShiftStart = editScheduleVM.ShiftStart;
            schedule.ShiftEnd = editScheduleVM.ShiftEnd;

            try
            {
                await _genericScheduleRepo.EditAsync();
                return true;
            }
            catch
            {
                throw;
            }

        }

        public async Task<IEnumerable<ScheduleVM>> GetAllSchedulesAsync()
        {
            try
            {
                var schedules = await _scheduleRepo.GetAllSchedulesAsync();
                return _mapper.Map<IEnumerable<ScheduleVM>>(schedules);
            } catch
            {
                throw;
            }
        }

        public async Task<ScheduleVM?> GetScheduleVMByUserIdAsync(string id)
        {
            try
            {
                var schedule = await _scheduleRepo.GetScheduleByUserIdAsync(id);
                return _mapper.Map<ScheduleVM>(schedule);
            }
            catch
            {
                throw;
            }
        }
        public async Task<ScheduleVM?> GetScheduleVMAsync(int id)
        {
            try
            {
                var schedule = await _genericScheduleRepo.GetByIdAsync(id);
                return _mapper.Map<ScheduleVM>(schedule);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RemoveScheduleAsync(int id)
        {
            try
            {
                await _genericScheduleRepo.DeleteAsync(id);
                return true;
            } catch
            {
                throw;
            }
        }
    }
}
