using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.ViewModels.Tablespace
{
    public class AddTableVM
    {
        public bool IsOccupied { get; set; } = false;
        public int Capacity { get; set; }
    }
}
