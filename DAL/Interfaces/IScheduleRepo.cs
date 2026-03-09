using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;

namespace RestaurantManagement.DAL.Interfaces
{
    public interface IScheduleRepo
    {
        public Task<IEnumerable<Schedule>> GetAllSchedulesAsync();
        public Task<Schedule?> GetScheduleByUserIdAsync(string id);
        /*
        public Task<Schedule?> GetScheduleAsync(int id);
        public Task<bool> AddScheduleAsync(Schedule schedule);
        public Task<bool> EditScheduleAsync();
        public Task<bool> DeleteScheduleAsync(int id);*/
    }
}
