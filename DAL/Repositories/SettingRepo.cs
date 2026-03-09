using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Models;
using RestaurantManagement.DAL.Data;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.DAL.Repositories
{
    public class SettingRepo(RestaurantDBContext context) : ISettingRepo
    {
        private protected RestaurantDBContext _context = context;
       /*
        public async Task EditSettingAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<SystemSetting?> GetSystemSettingAsync()
        {
            return await _context.SystemSetting.FirstOrDefaultAsync(s => s.Id == 1234);
        }
       */
    }
}
