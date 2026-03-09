using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Core.Models;
using RestaurantManagement.Core.ViewModels.Tablespace;

namespace RestaurantManagement.BLL.Interfaces
{
    public interface ITableServices
    {
        public Task<IEnumerable<TablespaceVM>> GetTablesAsync();
        public Task<TablespaceVM?> GetTableAsync(int id);
        public Task<TablespaceVM?> GetTableWithOrdersAsync(int id);
        public Task<TablespaceVM?> GetTableWithMenuItemsAsync(int id);
        public Task<bool> AddTableAsync(AddTableVM tableVM);
        public Task<bool> UpdateTableAsync(int id, EditTableVM tableVM);
        public Task<bool> DeleteTableAsync(int id);
        public EditTableVM EditVMMapper(TablespaceVM tableVM);
    }
}
