using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.SystemSetting;

namespace RestaurantManagement.BLL.Interfaces
{
    public interface ISettingServices
    {
        public Task<SystemSettingVM?> GetSystemSettingAsync();
        public Task<SystemSettingVM?> EditSettingAsync(EditSettingVM settingVM);
        public EditSettingVM EditVMMapper(SystemSettingVM settingVM);
    }
}
