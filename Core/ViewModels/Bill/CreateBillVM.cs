using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.ViewModels.Bill
{
    public class CreateBillVM
    {
        public string PaymentType { get; set; } = "";
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal VAT { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int TablespaceId { get; set; }
    }
}
