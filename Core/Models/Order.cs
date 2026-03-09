namespace RestaurantManagement.Core.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public bool IsModified { get; set; }
        public int Modification { get; set; } = 0;
        public string? Status { get; set; } = "";
        public string? Priority { get; set; } = "";
        public string? UserId { get; set; }
        public User? User { get; set; }
        public int TablespaceId { get; set; }
        public Tablespace? Tablespace { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = default!;
    }
}
