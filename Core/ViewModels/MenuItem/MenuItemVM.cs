using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.ViewModels.Category;

namespace RestaurantManagement.Core.ViewModels.MenuItem
{
    public class MenuItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int CategoryId { get; set; }
        public CategoryVM? CategoryVM { get; set; }
    }
}
