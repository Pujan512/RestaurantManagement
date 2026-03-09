using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.ViewModels.Tablespace
{
    public class EditTableVM
    {
        public int Id { get; set; }
        public bool IsOccupied { get; set; }
        public int Capacity { get; set; }
    }
}
