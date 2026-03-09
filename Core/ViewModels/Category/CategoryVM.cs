using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.ViewModels.MenuItem;

namespace RestaurantManagement.Core.ViewModels.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public ICollection<MenuItemVM>? MenuItemVMs { get; set; } = default!;
    }
}
