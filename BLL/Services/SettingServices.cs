using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RestaurantManagement.BLL.Interfaces;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.SystemSetting;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.BLL.Services
{
    public class SettingServices(IGenericRepository<SystemSetting> genericSettingRepo, IMapper mapper) : ISettingServices
    {
        private readonly IGenericRepository<SystemSetting> _genericSettingRepo = genericSettingRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<SystemSettingVM?> EditSettingAsync(EditSettingVM editSettingVM)
        {
            var setting = await _genericSettingRepo.GetByIdAsync(1234);
            if (setting == null) throw new Exception("Setting not found");

            setting.ModifiedAt = DateTime.Now;
            setting.ServiceCharge = editSettingVM.ServiceCharge;
            setting.VAT = editSettingVM.VAT;

            try
            {
                await _genericSettingRepo.EditAsync();
            }
            catch
            {
                throw new Exception("Unable to edit system setting in db");
            }
            return _mapper.Map<SystemSettingVM>(setting);
        }

        public async Task<SystemSettingVM?> GetSystemSettingAsync()
        {
            try
            {
                var setting = await _genericSettingRepo.GetByIdAsync(1234);
                if (setting == null) return null;
                return _mapper.Map<SystemSettingVM>(setting);
            } catch
            {
                throw new Exception("Unable to get system setting from db");
            }
        }

        public EditSettingVM EditVMMapper(SystemSettingVM settingVM)
        {
            return new EditSettingVM()
            {
                ServiceCharge = settingVM.ServiceCharge,
                VAT = settingVM.VAT,
            };
        }
    }
}
