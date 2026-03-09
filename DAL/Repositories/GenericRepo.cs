using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantManagement.DAL.Data;
using RestaurantManagement.DAL.Interfaces;

namespace RestaurantManagement.DAL.Repositories
{
    public class GenericRepo<T>(RestaurantDBContext context) : IGenericRepository<T> where T : class
    {
        private readonly RestaurantDBContext _context = context;
        private readonly DbSet<T> _table = context.Set<T>();
        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            _table.Remove(entity);
            await SaveAsync();
        }

        public async Task EditAsync()
        {
            await SaveAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
