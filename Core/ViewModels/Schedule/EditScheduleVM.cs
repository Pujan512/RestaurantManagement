using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.ViewModels.Schedule
{
    public class EditScheduleVM
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public TimeOnly ShiftStart { get; set; }
        public TimeOnly ShiftEnd { get; set; }
    }
}
