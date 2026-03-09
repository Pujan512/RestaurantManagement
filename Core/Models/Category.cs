namespace RestaurantManagement.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public ICollection<MenuItem>? MenuItems { get; set; } = default!;
    }
}
