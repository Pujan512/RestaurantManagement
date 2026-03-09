namespace RestaurantManagement.Core.Models
{
    public class Tablespace
    {
        public int Id { get; set; }
        public int TableNumber { get; set; }
        public bool IsOccupied { get; set; }
        public int Capacity { get; set; }
        public ICollection<Order> Orders { get; set; } = default!;
    }
}
