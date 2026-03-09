using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Core.ViewModels.SystemSetting
{
    public class EditSettingVM
    {
        public int Id { get; set; }
        public decimal VAT { get; set; }
        public decimal ServiceCharge { get; set; }
    }
}
