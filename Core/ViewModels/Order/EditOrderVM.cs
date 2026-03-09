using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.ViewModels.Order
{
    public class EditOrderVM
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public bool IsModified { get; set; } 
        public string? Status { get; set; } = "Pending";
        public string? Priority { get; set; } = "Medium";
        public int TablespaceId { get; set; }
    }
}
