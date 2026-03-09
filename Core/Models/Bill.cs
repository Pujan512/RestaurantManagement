namespace RestaurantManagement.Core.Models
{
    public class Bill
    {
        public int Id { get; set; }
        public string PaymentType { get; set; } = "";
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal ServiceCharge { get; set; }

        public decimal ServiceChargePercent { get; set; }

        public decimal VATPercent { get; set; }
        public decimal VAT { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int TablespaceId { get; set; }
        public Tablespace Tablespace { get; set; } = default!;
        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}
