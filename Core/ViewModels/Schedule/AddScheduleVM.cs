using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.ViewModels.Account;

namespace RestaurantManagement.Core.ViewModels.Schedule
{
    public class AddScheduleVM
    {
        public string UserId { get; set; } = "";
        public TimeOnly ShiftStart { get; set; } = new TimeOnly(10,00);
        public TimeOnly ShiftEnd { get; set; } = new TimeOnly(18,00);
    }
}
