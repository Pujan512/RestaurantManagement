using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.ViewModels.Order;

namespace RestaurantManagement.Core.ViewModels.Tablespace
{
    public class TablespaceVM
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public bool IsOccupied { get; set; }
        public int Capacity { get; set; }
        public List<OrderVM> OrderVMs { get; set; } = new();
    }
}
