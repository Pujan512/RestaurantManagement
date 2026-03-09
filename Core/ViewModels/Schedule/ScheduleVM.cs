using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Account;

namespace RestaurantManagement.Core.ViewModels.Schedule
{
    public class ScheduleVM
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public UserVM? UserVM { get; set; }
        public TimeOnly ShiftStart { get; set; }
        public TimeOnly ShiftEnd { get; set; }
    }
}
