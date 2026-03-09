using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.ViewModels.Account;
using RestaurantManagement.Core.ViewModels.OrderItem;
using RestaurantManagement.Core.ViewModels.Tablespace;

namespace RestaurantManagement.Core.ViewModels.Order
{
    public class OrderVM
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public bool IsModified { get; set; }
        public string Status { get; set; } = "";
        public string Priority { get; set; } = "";
        public string? UserId { get; set; }
        public UserVM? UserVM { get; set; }
        public int TablespaceId { get; set; }
        public TablespaceVM? Tablespace { get; set; }
        public ICollection<OrderItemVM> OrderItemVMs { get; set; } = default!;
    }
}
