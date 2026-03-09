using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.MenuItem;
using RestaurantManagement.Core.ViewModels.Order;
using RestaurantManagement.Core.ViewModels.Tablespace;

namespace RestaurantManagement.Core.ViewModels.Bill
{
    public class BillVM
    {
        public int Id { get; set; }
        public int TablespaceId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal ServiceChargePercent { get; set; }
        public decimal VAT { get; set; }
        public decimal VATPercent { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string? PaymentType { get; set; }
        public TablespaceVM Tablespace { get; set; } = default!;
        public IEnumerable<OrderVM> OrderVMs { get; set; } = new List<OrderVM>();
    }
}
