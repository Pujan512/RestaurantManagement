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
    public class TableRepo(RestaurantDBContext context) : ITableRepo
    {
        private readonly RestaurantDBContext _context = context;
        /*
        public async Task AddTableAsync(Tablespace tablespace)
        {
            await _context.Tablespace.AddAsync(tablespace);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int id)
        {
            var tablespace = await GetTableAsync(id);
            if (tablespace == null) throw new Exception("Tablespace not found");
            _context.Remove(tablespace);
            await _context.SaveChangesAsync();
        }

        public Task<Tablespace?> GetTableAsync(int id)
        {
            return _context.Tablespace.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tablespace>> GetTablesAsync()
        {
            return await _context.Tablespace.AsNoTracking().ToListAsync();
        }

        public async Task UpdateTableAsync()
        {
            await _context.SaveChangesAsync();
        }
        */

        public Task<Tablespace?> GetTableWithOrdersAsync(int id)
        {
            return _context.Tablespace.Include(t => t.Orders)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
        public Task<Tablespace?> GetTableWithMenuItemsAsync(int id)
        {
            return _context.Tablespace
                .Include(t => t.Orders)
                .ThenInclude(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
