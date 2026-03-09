using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.ViewModels.SystemSetting
{
    public class SystemSettingVM
    {
        public decimal VAT { get; set; }
        public decimal ServiceCharge { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
