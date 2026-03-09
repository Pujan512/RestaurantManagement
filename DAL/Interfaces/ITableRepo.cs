using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;

namespace RestaurantManagement.DAL.Interfaces
{
    public interface ITableRepo
    {
        /*
        public Task<IEnumerable<Tablespace>> GetTablesAsync();
        public Task<Tablespace?> GetTableAsync(int id);
        public Task AddTableAsync(Tablespace tablespace);
        public Task UpdateTableAsync();
        public Task DeleteTableAsync(int id);
        */
        public Task<Tablespace?> GetTableWithOrdersAsync(int id);
        public Task<Tablespace?> GetTableWithMenuItemsAsync(int id);
    }
}
