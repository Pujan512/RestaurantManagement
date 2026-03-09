using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.ViewModels.Order
{
    public class AddOrderVM
    {
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public string Priority { get; set; } = "Medium";
        public bool IsModified { get; set; } = false;
        public int UserId { get; set; }
    }
}
