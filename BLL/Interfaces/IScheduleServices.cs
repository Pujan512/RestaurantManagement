using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.ViewModels;
using RestaurantManagement.Core.ViewModels.Schedule;

namespace RestaurantManagement.BLL.Interfaces
{
    public interface IScheduleServices
    {
        public Task<IEnumerable<ScheduleVM>> GetAllSchedulesAsync();
        public Task<ScheduleVM?> GetScheduleVMAsync(int id);
        public Task<ScheduleVM?> GetScheduleVMByUserIdAsync(string id);
        public Task<bool> AddScheduleAsync(AddScheduleVM scheduleVM);
        public Task<bool> RemoveScheduleAsync(int id);
        public Task<bool> EditScheduleAsync(int id, EditScheduleVM scheduleVM);
    }
}
