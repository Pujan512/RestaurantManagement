using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.ViewModels.MenuItem;

namespace RestaurantManagement.Core.ViewModels.OrderItem
{
    public class OrderItemVM
    {
        public int Id { get; set; }
        public int MenuItemId { get; set; }
        public MenuItemVM? MenuItemVM { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
