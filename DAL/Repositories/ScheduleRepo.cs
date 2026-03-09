using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Core.Models;
using RestaurantManagement.DAL.Data;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.DAL.Repositories
{
    public class ScheduleRepo(RestaurantDBContext context) : IScheduleRepo
    {
        private readonly RestaurantDBContext _context = context;
        /*
        public async Task<bool> AddScheduleAsync(Schedule schedule)
        {
            await _context.Schedule.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteScheduleAsync(int id)
        {
            var schedule = await GetScheduleAsync(id);
            if(schedule == null) throw new Exception("Schedule not found");

            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditScheduleAsync()
        {
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Schedule?> GetScheduleAsync(int id)
        {
            return await _context.Schedule.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == id);
        }
        */

        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            return await _context.Schedule.AsNoTracking().Include(s => s.User).ToListAsync();
        }

        public async Task<Schedule?> GetScheduleByUserIdAsync(string id)
        {
            return await _context.Schedule.FirstOrDefaultAsync(s => s.UserId == id);
        }

    }
}
